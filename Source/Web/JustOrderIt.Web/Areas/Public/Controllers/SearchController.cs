namespace JustOrderIt.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Common.GlobalConstants;
    using Data.Models;
    using Hangfire;
    using Infrastructure.BackgroundWorkers;
    using Infrastructure.Caching;
    using Infrastructure.Extensions;
    using Infrastructure.Validators;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using Services.Logic;
    using Services.Logic.ServiceModels;
    using Services.Web;
    using Services.Web.CacheServices;
    using ViewModels.Categories;
    using ViewModels.Products;
    using ViewModels.Search;

    public class SearchController : BasePublicController, IBackgroundJobSubscriber
    {
        private readonly IAutoUpdateCacheService autoUpdateCacheService;
        private readonly IProductsService productsService;
        private readonly ISearchFiltersService searchFiltersService;
        private readonly ICategoriesService categoriesService;
        private readonly IKeywordsService keywordsService;

        public SearchController(
            IAutoUpdateCacheService autoUpdateCacheService,
            IProductsService productsService,
            ISearchFiltersService searchFiltersService,
            ICategoriesService categoriesService,
            IKeywordsService keywordsService)
        {
            this.autoUpdateCacheService = autoUpdateCacheService;
            this.productsService = productsService;
            this.searchFiltersService = searchFiltersService;
            this.categoriesService = categoriesService;
            this.keywordsService = keywordsService;
        }

        [HttpPost]
        public ActionResult SearchBar(SearchViewModel searchViewModel)
        {
            if (searchViewModel.CategoryId != null && searchViewModel.CategoryId > 0)
            {
                return this.RedirectToAction("SearchByCategory", "Search", new { categoryId = (int)searchViewModel.CategoryId, query = searchViewModel.Query , area = Areas.PublicAreaName });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(searchViewModel.Query))
                {
                    return this.RedirectToAction("Index", "Home", new { area = Areas.PublicAreaName });
                }
                else
                {
                    return this.RedirectToAction("SearchByQuery", "Search", new { query = searchViewModel.Query, area = Areas.PublicAreaName });
                }
            }
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult SearchAutoComplete(string prefix)
        {
            var keywords = this.autoUpdateCacheService.Get<List<string>, SearchController>(
                CacheConstants.AllCategoriesKeywords,
                () => this.GetKeywords(),
                CacheConstants.KeywordsForAutoCompleteCacheExpiration,
                "GetAndCacheKeywords",
                new object[] { CacheConstants.AllCategoriesKeywords },
                CacheConstants.KeywordsForAutoCompleteUpdateBackgroundJobDelay
                );

            var results = keywords.Where(kw => kw.ToLower().StartsWith(prefix.ToLower())).ToList();

            return this.Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchByQuery(string query)
        {
            var viewModel = new QuerySearchViewModel { Query = query };

            var categories = this.categoriesService.GetAll().ToList();

            foreach (var category in categories)
            {
                var categoryProducts = this.GetCachedProductsOfCategory(category.Id)
                    .FilterProductsBySearchQuery(query)
                    .Take(4)
                    .AsQueryable()
                    .To<ProductForQuerySearchViewModel>()
                    .ToList();

                var categoryFilters = category.SearchFilters.AsQueryable().To<SearchFilterForCategoryViewModel>().ToList();

                var categoryModel = new CategoryForQuerySearchViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Products = categoryProducts,
                    SearchFilters = categoryFilters
                };

                viewModel.CategoriesData.Add(categoryModel);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult SearchByCategory(int categoryId, string query, long searchOptionsBitMask = 0)
        {
            var model = new CategorySearchViewModel
            {
                CategoryId = categoryId,
                Query = query
            };

            var searchFilters = this.categoriesService.GetById(categoryId).SearchFilters.AsQueryable().To<SearchFilterForCategoryViewModel>().OrderBy(sf => sf.Id).ToList();
            this.PopulateSearchFilterOptionsFromBitMask(searchOptionsBitMask, searchFilters);
            model.SearchFilters = searchFilters;

            model.Products = this.GetCachedProductsOfCategory(categoryId)
                .FilterProductsByRefinementOptions(searchFilters.AsQueryable().To<SearchFilterRefinementModel>().ToList())
                .FilterProductsBySearchQuery(query)
                .AsQueryable()
                .To<ProductForCategorySearchViewModel>()
                .ToList();

            return this.View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RefineCategorySearch(int categoryId, string query, List<SearchFilterForCategoryViewModel> searchFilters)
        {
            long searchOptionsBitMask = 0;
            if (ModelState.IsValid)
            {
                searchOptionsBitMask = ExtractBitMaskFromSearchFilterOptions(searchFilters);
            }

            return this.RedirectToAction("SearchByCategory", "Search", new { categoryId = categoryId, query = query, searchOptionsBitMask = searchOptionsBitMask });
        }

        [HttpGet]
        public ActionResult AllProductsWithTag(string tag)
        {
            var products = this.productsService.GetAll().Where(p => p.Tags.Select(t => t.Name).Contains(tag));
            var result = products.To<ProductWithTagViewModel>();
            return this.View(result);
        }

        #region Helpers
            #region Background jobs workers
        public void BackgroundOperation(string methodName, object[] args)
        {
            var methodInfo = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo.Invoke(this, args);
        }

        /// <summary>
        /// A Backgroung job worker fetching updated data to be subsequently cached. The parameters are of types Json.NET parses to, and then are
        /// being converted to the desired types. Otherwise the deserialization process triggered by Hangfire will fail. For example, if we expect Int32 parameter
        /// and supply an Int32 volue, Json.NET would parse it to Int64 and the following exception is thrown:
        /// "Object of type 'System.Int64' cannot be converted to type 'System.Int32'..."
        /// </summary>
        /// <param name="cacheKey">The key of the cache object in HttpRuntime.Cache</param>
        /// <param name="categoryId">The Id of the category for which we gather data.</param>
        [AutomaticRetry(Attempts = 0)]
        [NonAction]
        private void GetAndCacheProductsOfCategory(string cacheKey, [Range(1, int.MaxValue, ErrorMessage = "Invalid category Id passed to the background worker.")]long categoryId)
        {
            var updatedData = this.GetProductsOfCategory((int)categoryId);
            this.autoUpdateCacheService.UpdateAuxiliaryCacheValue(cacheKey, updatedData);
        }

        [NonAction]
        private List<ProductCacheViewModel> GetProductsOfCategory(int categoryId)
        {
            var result = this.categoriesService.GetById(categoryId).Products
                .AsQueryable()
                .To<ProductCacheViewModel>()
                .ToList();

            return result;
        }

        [AutomaticRetry(Attempts = 0)]
        [NonAction]
        private void GetAndCacheKeywords(string cacheKey)
        {
            var updatedData = this.GetKeywords();
            this.autoUpdateCacheService.UpdateAuxiliaryCacheValue(cacheKey, updatedData);
        }

        [NonAction]
        private List<string> GetKeywords()
        {
            return this.keywordsService.GetAll().Select(k => k.SearchTerm).ToList();
        }

        [NonAction]
        private List<ProductCacheViewModel> GetCachedProductsOfCategory(int categoryId)
        {
            var cacheKey = "category" + categoryId.ToString() + "products";
            var cachedProducts = this.autoUpdateCacheService.Get<List<ProductCacheViewModel>, SearchController>(
                cacheKey: cacheKey,
                dataFunc: () => this.GetProductsOfCategory(categoryId),
                absoluteExpiration: CacheConstants.AllProductsInCategoryCacheExpiration,
                methodName: "GetAndCacheProductsOfCategory",
                methodArguments: new object[] { cacheKey, categoryId },
                updateJobDelay: CacheConstants.AllProductsInCategoryUpdateBackgroundJobDelay);

            return cachedProducts;
        }
            #endregion

            #region Bitmask workers
        [NonAction]
        private long ExtractBitMaskFromSearchFilterOptions(List<SearchFilterForCategoryViewModel> searchFilters)
        {
            long bitMask = 0;
            int searchFiltersCount = searchFilters.Count();
            for (int i = 0; i < searchFiltersCount; i++)
            {
                int filterOptionsCount = searchFilters[i].RefinementOptions.Count();
                for (int j = 0; j < filterOptionsCount; j++)
                {
                    bitMask = bitMask << 1;
                    if (searchFilters[i].RefinementOptions[j].Checked)
                    {
                        bitMask = bitMask | 1;
                    }
                }
            }

            return bitMask;
        }

        [NonAction]
        private void PopulateSearchFilterOptionsFromBitMask(long bitMask, List<SearchFilterForCategoryViewModel> searchFilters)
        {
            int searchFiltersCount = searchFilters.Count();

            for (int i = searchFiltersCount - 1; i >= 0; i--)
            {
                int filterOptionsCount = searchFilters[i].RefinementOptions.Count();
                for (int j = filterOptionsCount - 1; j >= 0; j--)
                {
                    searchFilters[i].RefinementOptions[j].Checked = (bitMask & 1) == 1;
                    bitMask = bitMask >> 1;
                }
            }
        }
            #endregion
        #endregion
    }
}
namespace MvcProject.Web.Areas.Public.Controllers
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
    using Common.GlobalConstants;
    using Data.Models;
    using Hangfire;
    using Infrastructure.BackgroundWorkers;
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
        private readonly ISearchFilterHelpers filterStringHelpers;
        //private readonly ISearchRefinementHelpers<ProductOfCategoryViewModel, SearchFilterForCategoryViewModel> searchAlgorithms;

        public SearchController(
            IAutoUpdateCacheService autoUpdateCacheService,
            IProductsService productsService,
            ISearchFiltersService searchFiltersService,
            ICategoriesService categoriesService,
            IKeywordsService keywordsService,
            ISearchFilterHelpers filterStringHelpers
            //ISearchRefinementHelpers<ProductOfCategoryViewModel, SearchFilterForCategoryViewModel> searchAlgorithms
            )
        {
            this.autoUpdateCacheService = autoUpdateCacheService;
            this.productsService = productsService;
            this.searchFiltersService = searchFiltersService;
            this.categoriesService = categoriesService;
            this.keywordsService = keywordsService;
            this.filterStringHelpers = filterStringHelpers;
            //this.searchAlgorithms = searchAlgorithms;
        }

        public ActionResult SearchByQuery(SearchViewModel searchViewModel)
        {
            if (searchViewModel.CategoryId != null && searchViewModel.CategoryId > 0)
            {
                return this.RedirectToAction("SearchByCategory", "Search", new { categoryId = searchViewModel.CategoryId, query = searchViewModel.Query, area = Areas.PublicAreaName });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(searchViewModel.Query))
                {
                    return this.RedirectToAction("Index", "Home", new { area = Areas.PublicAreaName });
                }

                IDictionary<int, List<ProductForQuerySearchViewModel>> resultsByCategory = this.FilterByQuery(searchViewModel.Query);

                var viewModel = this.productsService
                    .GetAll()
                    .Take(10)
                    .AsQueryable()
                    .FilterProductsBySearchTerm(searchViewModel.Query)
                    .To<ProductForQuerySearchViewModel>()
                    .ToList()
                    ;

                //return this.PartialView(viewModel);
                return this.View(viewModel);
            }
        }

        private IDictionary<int, List<ProductForQuerySearchViewModel>> FilterByQuery(string query)
        {
            var results = new Dictionary<int, List<ProductForQuerySearchViewModel>>();

            var separateQueryTerms = query.Split(new char[' '], StringSplitOptions.RemoveEmptyEntries);
            var termsCount = separateQueryTerms.Count();

            var categoriesRelevance = new SortedDictionary<int, int>();

            return null;
        }

        public JsonResult SearchAutoComplete(string prefix)
        {
            var cacheKey = "allCategoriesKeywords";
            var keywords = this.autoUpdateCacheService.Get<List<string>, SearchController>(
                cacheKey,
                () => this.GetKeywords(),
                CacheConstants.KeywordsForAutoCompleteCacheExpiration,
                "GetAndCacheKeywords",
                new object[] { cacheKey },
                CacheConstants.KeywordsForAutoCompleteUpdateBackgroundJobDelay
                );

            var results = keywords.Where(kw => kw.StartsWith(prefix.ToLower()));

            return this.Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchByCategory(int categoryId, string query)
        {
            var category = this.categoriesService.GetById(categoryId);
            var searchFilters = category.SearchFilters.AsQueryable().To<SearchFilterForCategoryViewModel>().ToList();
            var categorySearchViewModel = new CategorySearchViewModel() { Id = categoryId, SearchFilters = searchFilters, Query = query };

            return this.View(categorySearchViewModel);
        }

        //[HttpPost]
        //[AjaxOnly]
        public JsonResult ReadSearchResult(
            [DataSourceRequest]DataSourceRequest request,
            int categoryId,
            string query,
            IEnumerable<RefinementOption> refinementOptions
            //, IEnumerable<SearchFilterForCategoryViewModel> searchFilters
            //, RefinementOption searchFilter
            )
        {
            //var cacheKey = "category" + categoryId.ToString() + "products";
            //var allProductsInCategory = this.autoUpdateCacheService.Get<List</*ProductForCategorySearchViewModel*/ProductCacheViewModel>, SearchController>(
            //    cacheKey,
            //    () => this.GetProductsOfCategory(categoryId),
            //    CacheConstants.AllProductsInCategoryCacheExpiration,
            //    "GetAndCacheProductsOfCategory",
            //    new object[] { cacheKey, categoryId },
            //    CacheConstants.AllProductsInCategoryUpdateBackgroundJobDelay
            //    )
            //    .AsQueryable()
            //    .To<ProductForCategorySearchViewModel>()
            //    .ToList()
            //    ;

            var allProductsInCategory = this.FilterCategoryProducts(categoryId, query, refinementOptions);

            return this.Json(allProductsInCategory.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet); ;
        }

        private List<ProductForCategorySearchViewModel> FilterCategoryProducts(
            int categoryId,
            string query,
            IEnumerable<RefinementOption> refinementOptions)
        {
            var cacheKey = "category" + categoryId.ToString() + "products";
            var allProductsInCategory = this.autoUpdateCacheService.Get<List</*ProductForCategorySearchViewModel*/ProductCacheViewModel>, SearchController>(
                cacheKey,
                () => this.GetProductsOfCategory(categoryId),
                CacheConstants.AllProductsInCategoryCacheExpiration,
                "GetAndCacheProductsOfCategory",
                new object[] { cacheKey, categoryId },
                CacheConstants.AllProductsInCategoryUpdateBackgroundJobDelay
                )
                .AsQueryable()
                .To<ProductForCategorySearchViewModel>()
                .ToList()
                ;

            return allProductsInCategory;
        }

        public void BackgroundOperation(string methodName, object[] args)
        {
            var methodInfo = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo.Invoke(this, args);
        }

        [HttpGet]
        public ActionResult AllProductsWithTag(string tag)
        {
            var products = this.productsService.GetAll().Where(p => p.Tags.Select(t => t.Name).Contains(tag));
            var result = products.To<ProductWithTagViewModel>();
            return this.View(result);
        }

        //public ActionResult SessionTest()
        //{
        //    if (Session["datetime"] == null)
        //    {
        //        this.Session.Add("datetime", DateTime.Now.ToString());
        //    }

        //    return this.View("SessionTest");
        //}

        //public ActionResult TempDataTest()
        //{
        //    if (this.TempData["datetime"] == null)
        //    {
        //        this.TempData["datetime"] = DateTime.Now.ToString();
        //    }

        //    return this.View("TempDataTest");
        //}

        #region Helpers
            #region Background jobs workers
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
            //string cacheKey = Convert.ToString(args[0]);
            //int categoryId = Convert.ToInt32(args[1]);

            var updatedData = this.GetProductsOfCategory((int)categoryId);
            this.autoUpdateCacheService.UpdateAuxiliaryCacheValue(cacheKey, updatedData);
        }

        [NonAction]
        private List</*ProductForCategorySearchViewModel*/ProductCacheViewModel> GetProductsOfCategory(int categoryId)
        {
            var result = this.categoriesService.GetById(categoryId).Products
                //.Take(500)
                .AsQueryable()
                .To<ProductCacheViewModel>()
                .ToList()
                ;

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
            return this.keywordsService.GetAll().Select(k => k.SearchTerm/*.ToLower()*/).ToList();
        }
            #endregion

        [NonAction]
        private bool ValidateSearchFilter(RefinementOption searchFilter)
        {
            if (string.IsNullOrWhiteSpace(searchFilter.Value))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly ISearchFilterHelpers filterStringHelpers;
        //private readonly IBackgroundJobsService backgroundJobService;
        //private readonly ISearchRefinementHelpers<ProductOfCategoryViewModel, SearchFilterForCategoryViewModel> searchAlgorithms;

        public SearchController(
            IAutoUpdateCacheService autoUpdateCacheService,
            IProductsService productsService,
            ISearchFiltersService searchFiltersService,
            ICategoriesService categoriesService,
            ISearchFilterHelpers filterStringHelpers
            //IBackgroundJobsService backgroundJobService
            //ISearchRefinementHelpers<ProductOfCategoryViewModel, SearchFilterForCategoryViewModel> searchAlgorithms
            )
        {
            this.autoUpdateCacheService = autoUpdateCacheService;
            this.productsService = productsService;
            this.searchFiltersService = searchFiltersService;
            this.categoriesService = categoriesService;
            this.filterStringHelpers = filterStringHelpers;
            //this.backgroundJobService = backgroundJobService;
            //this.searchAlgorithms = searchAlgorithms;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Search(string query, int categoryId, IEnumerable<SearchFilterForCategoryViewModel> searchFilters)
        {
            return null;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult SearchQuery()
        {
            return null;
        }

        [HttpPost]
        [ChildActionOnly]
        public ActionResult SearchQuery(SearchQueryViewModel searchQuery)
        {
            return null;
        }

        [HttpGet]
        public ActionResult SearchByCategory(int categoryId)
        {
            var category = this.categoriesService.GetById(categoryId);
            var searchFilters = category.SearchFilters.AsQueryable().To<SearchFilterForCategoryViewModel>().ToList();
            var viewModel = new CategorySearchViewModel() { Id = categoryId, SearchFilters = searchFilters };

            return this.View("CategorySearchView", viewModel);
        }

        //[HttpPost]
        //[AjaxOnly]
        public JsonResult ReadSearchResult(
            [DataSourceRequest]DataSourceRequest request,
            int categoryId
            //, IEnumerable<SearchFilterForCategoryViewModel> searchFilters
            //, RefinementOption searchFilter
            )
        {
            var cacheKey = "category" + categoryId.ToString() + "products";
            var allProductsInCategory = this.autoUpdateCacheService.Get<List<ProductOfCategoryViewModel>, SearchController>(
                new object[] { categoryId, cacheKey },
                cacheKey,
                () => this.GetProductsOfCategory(categoryId),
                CacheConstants.AllProductsInCategoryCacheExpiration,
                CacheConstants.AllProductsInCategoryUpdateBackgroundJobDelay
                );

            var filteredProducts = allProductsInCategory;

            return this.Json(allProductsInCategory.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet); ;
        }

        [AutomaticRetry(Attempts = 0)]
        [NonAction]
        public void BackgroundOperation(object[] args)
        {
            int categoryId = Convert.ToInt32(args[0]);
            string updateCacheKey = Convert.ToString(args[1]);
            var updatedData = this.GetProductsOfCategory(categoryId);
            this.autoUpdateCacheService.UpdateAuxiliaryCacheValue(updateCacheKey, updatedData);
        }

        [OutputCache(Duration = 15 * 60, VaryByParam = "viewModel;categoryId")]
        [ChildActionOnly]
        public PartialViewResult RefineSearchBar(IEnumerable<SearchFilterForCategoryViewModel> viewModel/*, int categoryId*/)
        {
            //this.ViewData["categoryId"] = categoryId;
            return this.PartialView("_RefineSearchBar", viewModel);
        }

        [HttpGet]
        public ActionResult AllProductsWithTag(string tag)
        {
            var products = this.productsService.GetAll().Where(p => p.Tags.Select(t => t.Name).Contains(tag));
            var result = products.To<ProductWithTagViewModel>();
            return this.View(result);
        }

        public ActionResult SessionTest()
        {
            if (Session["datetime"] == null)
            {
                this.Session.Add("datetime", DateTime.Now.ToString());
            }

            return this.View("SessionTest");
        }

        //public ActionResult TempDataTest()
        //{
        //    if (this.TempData["datetime"] == null)
        //    {
        //        this.TempData["datetime"] = DateTime.Now.ToString();
        //    }

        //    return this.View("TempDataTest");
        //}

        #region Helpers
        [NonAction]
        private List<ProductOfCategoryViewModel> GetProductsOfCategory(int categoryId)
        {
            var result = this.categoriesService.GetById(categoryId).Products
                .Take(500)
                .AsQueryable()
                .To<ProductOfCategoryViewModel>()
                .ToList();

            return result;
        }

        [NonAction]
        private bool ValidateSearchFilter(RefinementOption searchFilter)
        {
            if (string.IsNullOrWhiteSpace(searchFilter.SelectedValue))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
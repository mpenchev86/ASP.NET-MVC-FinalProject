namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Extensions;
    using Infrastructure.Validators;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using Services.Logic;
    using Services.Logic.ServiceModels;
    using Services.Web;
    using ViewModels.Categories;
    using ViewModels.Products;
    using ViewModels.Search;

    public class SearchController : BasePublicController
    {
        private IProductsService productsService;
        private ISearchFiltersService searchFiltersService;
        private ICategoriesService categoriesService;
        private ISearchFilterHelpers filterStringHelpers;
        //private ISearchRefinementHelpers<ProductOfCategoryViewModel, SearchFilterForCategoryViewModel> searchAlgorithms;

        public SearchController(
            ICacheService cacheService,
            IProductsService productsService,
            ISearchFiltersService searchFiltersService,
            ICategoriesService categoriesService,
            ISearchFilterHelpers filterStringHelpers
            //ISearchRefinementHelpers<ProductOfCategoryViewModel, SearchFilterForCategoryViewModel> searchAlgorithms
            )
        {
            this.Cache = cacheService;
            this.productsService = productsService;
            this.searchFiltersService = searchFiltersService;
            this.categoriesService = categoriesService;
            this.filterStringHelpers = filterStringHelpers;
            //this.searchAlgorithms = searchAlgorithms;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult AllProductsOfCategory(int categoryId)
        {
            var category = this.categoriesService.GetById(categoryId);
            //var products = category.Products.AsQueryable().To<ProductOfCategoryViewModel>().ToList();
            var searchFilters = category.SearchFilters.AsQueryable().To<SearchFilterForCategoryViewModel>().ToList();
            var viewModel = new CategorySearchViewModel() { Id = categoryId, /*Products = products,*/ SearchFilters = searchFilters };

            return this.View("CategoryOverView", viewModel);
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
            var category = this.categoriesService.GetById(categoryId);
            var products = this.Cache.Get(
                "category" + categoryId.ToString() + "products",
                () => category.Products
                    //.Take(200)
                    .AsQueryable()
                    //.FilterProducts(searchFilter)
                    .To<ProductOfCategoryViewModel>()
                    .ToList(),
                30 * 60,
                true,
                21 * 60);

            return this.Json(products.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet); ;
        }

        [OutputCache(Duration = 15 * 60, VaryByParam = "viewModel;categoryId")]
        [ChildActionOnly]
        public PartialViewResult RefineSearchBar(IEnumerable<SearchFilterForCategoryViewModel> viewModel, int categoryId)
        {
            this.ViewData["categoryId"] = categoryId;
            return this.PartialView("_RefineSearchBar", viewModel);
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
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
    using ViewModels.Categories;
    using ViewModels.Products;
    using ViewModels.Search;

    public class SearchController : BasePublicController
    {
        private IProductsService productsService;
        private ISearchFiltersService searchFiltersService;
        private ICategoriesService categoriesService;
        private IProductSearchAlgorithms searchAlgorithms;

        public SearchController(
            IProductsService productsService,
            ISearchFiltersService searchFiltersService,
            ICategoriesService categoriesService,
            IProductSearchAlgorithms searchAlgorithms)
        {
            this.productsService = productsService;
            this.searchFiltersService = searchFiltersService;
            this.categoriesService = categoriesService;
            this.searchAlgorithms = searchAlgorithms;
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

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public JsonResult ReadSearchResult([DataSourceRequest]DataSourceRequest request, int categoryId)
        {
            var category = this.categoriesService.GetById(categoryId);
            var products = category.Products.AsQueryable().To<ProductOfCategoryViewModel>().ToList();

            return this.Json(products.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet); ;
        }

        //[HttpPost]
        public ActionResult ProcessFilterSelection(int filterId, SearchFilterOptionsType optionsType, string selectedOption)
        {
            return null;
        }

        [HttpGet]
        public ActionResult AllProductsWithTag(string tag)
        {
            var products = this.productsService.GetAll().Where(p => p.Tags.Select(t => t.Name).Contains(tag));
            var result = products.To<ProductWithTagViewModel>();
            return this.View(result);
        }

    }
}
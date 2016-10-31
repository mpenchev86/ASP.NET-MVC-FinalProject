namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.Extensions;
    using Services.Data;
    using ViewModels.Categories;
    using ViewModels.Products;

    public class SearchController : BasePublicController
    {
        private IProductsService productsService;
        private ISearchFiltersService searchFiltersService;

        public SearchController(
            IProductsService productsService,
            ISearchFiltersService searchFiltersService)
        {
            this.productsService = productsService;
            this.searchFiltersService = searchFiltersService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult AllProductsOfCategory()
        {
            var viewModel = (CategorySearchViewModel)this.TempData["CategorySearchData"];
            return this.View("CategoryOverView", viewModel);
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
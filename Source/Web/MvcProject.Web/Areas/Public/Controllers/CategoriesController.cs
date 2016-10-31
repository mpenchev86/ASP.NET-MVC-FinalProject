namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.Extensions;
    using Services.Data;
    using Services.Web;
    using ViewModels.Categories;
    using ViewModels.Products;
    using ViewModels.Search;

    public class CategoriesController : BasePublicController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(
            ICacheService cacheService,
            ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
            this.Cache = cacheService;
        }

        [HttpGet]
        public ActionResult GetCategoriesForDropDown()
        {
            var categories = this.Cache.Get(
                "categoriesForLayoutDropDown",
                () => this.categoriesService
                    .GetAll()
                    //.Select(c => new SelectListItem
                    //{
                    //    Value = c.Id.ToString(),
                    //    Text = c.Name
                    //})
                    .To<CategoryForLayoutDropDown>()
                    .OrderBy(i => i.Name)
                    .ToList());

            return this.PartialView("_CategoriesDropDown", categories);
        }

        [HttpGet]
        public ActionResult CategoryOverView(int categoryId)
        {
            var category = this.categoriesService.GetById(categoryId);
            var products = category.Products.AsQueryable().To<ProductOfCategoryViewModel>().ToList();
            var searchFilters = category.SearchFilters.AsQueryable().To<SearchFiltersForCategoryViewModel>().ToList();
            var viewModel = new CategorySearchViewModel() { Products = products, SearchFilters = searchFilters };
            this.TempData.Add("CategorySearchData", viewModel);

            return this.RedirectToAction("AllProductsOfCategory", "Search");
        }
    }
}
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
                    .To<CategoryForLayoutDropDown>()
                    .OrderBy(i => i.Name)
                    .ToList()
                , 30 * 60
                );

            return this.PartialView("_CategoriesDropDown", categories);
        }
    }
}
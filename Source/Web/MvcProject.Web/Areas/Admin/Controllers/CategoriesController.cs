namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using GlobalConstants;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Categories;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class CategoriesController : BaseGridController<ICategoriesService, CategoryViewModel>
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
            : base(categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Save record to base
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Edit record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            //if (viewModel != null)
            //{
            //    // Destroy record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Destroy(request, viewModel);
        }
    }
}
namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using MvcProject.Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Categories;
    using ViewModels.Products;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class CategoriesController : BaseGridController<Category, CategoryViewModel, ICategoriesService, int>
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;
        private readonly ISearchFiltersService searchFiltersService;

        public CategoriesController(
            ICategoriesService categoriesService,
            IProductsService productsService,
            ISearchFiltersService searchFiltersService)
            : base(categoriesService)
        {
            this.categoriesService = categoriesService;
            this.productsService = productsService;
            this.searchFiltersService = searchFiltersService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region Data Workers
        protected override void PopulateEntity(Category entity, CategoryViewModel viewModel)
        {
            if (viewModel.Products != null)
            {
                foreach (var product in viewModel.Products)
                {
                    entity.Products.Add(this.productsService.GetById(product.Id));
                }
            }

            if (viewModel.SearchFilters != null)
            {
                foreach (var searchFilter in viewModel.SearchFilters)
                {
                    entity.SearchFilters.Add(this.searchFiltersService.GetById(searchFilter.Id));
                }
            }

            entity.Name = viewModel.Name;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        protected override IEnumerable<CategoryViewModel> GetDataAsEnumerable()
        {
            return this.categoriesService.GetAll().To<CategoryViewModel>().OrderBy(x => x.Name);
        }
        #endregion
    }
}
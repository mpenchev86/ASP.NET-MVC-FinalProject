namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common.GlobalConstants;
    using Data.Models;
    using Infrastructure.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Categories;
    using ViewModels.SearchFilters;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class SearchFiltersController : BaseGridController<SearchFilter, SearchFilterViewModel, ISearchFiltersService, int>
    {
        private readonly ISearchFiltersService searchFiltersService;
        private readonly ICategoriesService categoriesService;

        public SearchFiltersController(
            ISearchFiltersService searchFiltersService,
            ICategoriesService categoriesService)
            : base(searchFiltersService)
        {
            this.searchFiltersService = searchFiltersService;
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var foreignKeys = new SearchFilterViewModelForeignKeys
            {
                Categories = this.categoriesService.GetAll().To<CategoryDetailsForSearchFilterViewModel>().ToList(),
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, SearchFilterViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, SearchFilterViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, SearchFilterViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region Data Workers
        protected override void PopulateEntity(SearchFilter entity, SearchFilterViewModel viewModel)
        {
            entity.Name = viewModel.Name;
            entity.Options = viewModel.Options;
            entity.MeasureUnit = viewModel.MeasureUnit;
            entity.OptionsType = viewModel.OptionsType;
            entity.SelectionType = viewModel.SelectionType;
            entity.CategoryId = viewModel.CategoryId;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        protected override IQueryable<SearchFilterViewModel> GetQueryableData()
        {
            return this.searchFiltersService.GetAll().To<SearchFilterViewModel>().OrderBy(x => x.Name);
        }
        #endregion
    }
}
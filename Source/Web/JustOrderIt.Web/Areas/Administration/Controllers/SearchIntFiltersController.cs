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
    using Services.Data.Contracts;
    using ViewModels.Categories;
    using ViewModels.SearchFilters;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class SearchIntFiltersController : BaseGridController<SearchIntFilter, SearchIntFilterViewModel, ISearchIntFiltersService, int>
    {
        private readonly ISearchIntFiltersService searchFiltersService;
        private readonly ICategoriesService categoriesService;

        public SearchIntFiltersController(
            ISearchIntFiltersService searchFiltersService,
            ICategoriesService categoriesService)
            : base(searchFiltersService)
        {
            this.searchFiltersService = searchFiltersService;
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var foreignKeys = new SearchIntFilterViewModelForeignKeys
            {
                Categories = this.categoriesService.GetAll().To<CategoryDetailsForSearchFilterViewModel>(),
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
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, SearchIntFilterViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, SearchIntFilterViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, SearchIntFilterViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region Data Workers
        protected override void PopulateEntity(SearchIntFilter entity, SearchIntFilterViewModel viewModel)
        {
            entity.Name = viewModel.Name;
            entity.Options = viewModel.Options;
            entity.OptionsMeasureUnit = viewModel.OptionsMeasureUnit;
            entity.Type = viewModel.Type;
            entity.CategoryId = viewModel.CategoryId;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        protected override IEnumerable<SearchIntFilterViewModel> GetDataAsEnumerable()
        {
            return this.searchFiltersService.GetAll().To<SearchIntFilterViewModel>().OrderBy(x => x.Name);
        }
        #endregion
    }
}
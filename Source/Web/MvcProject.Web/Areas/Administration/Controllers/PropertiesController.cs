namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using MvcProject.Common.GlobalConstants;
    using Infrastructure.Caching;
    using Infrastructure.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Descriptions;
    using ViewModels.Properties;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class PropertiesController : BaseGridController<Property, PropertyViewModel, IPropertiesService, int>
    {
        private readonly IPropertiesService propertiesService;
        private readonly IDescriptionsService descriptionsService;

        public PropertiesController(
            IPropertiesService propertiesService,
            IDescriptionsService descriptionsService)
            : base(propertiesService)
        {
            this.propertiesService = propertiesService;
            this.descriptionsService = descriptionsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var foreignKeys = new PropertyViewModelForeignKeys
            {
                Descriptions = this.descriptionsService.GetAll().To<DescriptionDetailsForPropertyViewModel>()
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
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, PropertyViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, PropertyViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PropertyViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region DataProviders
        protected override void PopulateEntity(Property entity, PropertyViewModel viewModel)
        {
            entity.Name = viewModel.Name;
            entity.Value = viewModel.Value;
            entity.DescriptionId = viewModel.DescriptionId;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        [HttpGet]
        protected override IEnumerable<PropertyViewModel> GetDataAsEnumerable()
        {
            return this.propertiesService.GetAll().To<PropertyViewModel>().OrderBy(p => p.Name);
        }
        #endregion
    }
}
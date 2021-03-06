﻿namespace JustOrderIt.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using JustOrderIt.Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Descriptions;
    using ViewModels.Products;
    using ViewModels.Properties;
    using Data.Models.Catalog;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class DescriptionsController : BaseGridController<Description, DescriptionViewModel, IDescriptionsService, int>
    {
        private readonly IDescriptionsService descriptionsService;
        private readonly IPropertiesService propertiesService;

        public DescriptionsController(
            IDescriptionsService descriptionsService,
            IPropertiesService propertiesService)
            : base(descriptionsService)
        {
            this.descriptionsService = descriptionsService;
            this.propertiesService = propertiesService;
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
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, DescriptionViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, DescriptionViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, DescriptionViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region Data Workers
        protected override void PopulateEntity(Description entity, DescriptionViewModel viewModel)
        {
            var descriptionPropertiesIds = viewModel.Properties.Select(c => c.Id);
            entity.Properties = this.propertiesService.GetAll().Where(pr => descriptionPropertiesIds.Contains(pr.Id)).ToList();

            entity.Content = viewModel.Content;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }
        #endregion
    }
}
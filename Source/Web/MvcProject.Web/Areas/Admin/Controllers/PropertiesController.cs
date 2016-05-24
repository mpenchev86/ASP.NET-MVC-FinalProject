namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    using GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Descriptions;
    using ViewModels.Properties;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class PropertiesController : BaseGridController<PropertyViewModel, IPropertiesService>
    {
        private readonly IPropertiesService propertiesService;
        private readonly IDescriptionsService descriptionsService;

        public PropertiesController(IPropertiesService propertiesService, IDescriptionsService descriptionsService)
            : base(propertiesService)
        {
            this.propertiesService = propertiesService;
            this.descriptionsService = descriptionsService;
        }

        public ActionResult Index()
        {
            var foreignKeys = new PropertyForeignKeysViewModel
            {
                Descriptions = this.descriptionsService.GetAll().To<DescriptionDetailsForPropertyViewModel>().ToList()
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, PropertyViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Save record to base
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, PropertyViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Edit record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PropertyViewModel viewModel)
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
namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Descriptions;
    using ViewModels.Properties;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class DescriptionsController : BaseGridController<Description, DescriptionViewModel, IDescriptionsService, int>
    {
        private readonly IDescriptionsService descriptionsService;
        private readonly IPropertiesService propertiesService;

        public DescriptionsController(IDescriptionsService descriptionsService, IPropertiesService propertiesService)
            : base(descriptionsService)
        {
            this.descriptionsService = descriptionsService;
            this.propertiesService = propertiesService;
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
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, DescriptionViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, DescriptionViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, DescriptionViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

#region DataProviders
        protected override void PopulateEntity(Description entity, DescriptionViewModel viewModel)
        {
            if (viewModel.Properties != null)
            {
                foreach (var property in viewModel.Properties)
                {
                    entity.Properties.Add(this.propertiesService.GetById(property.Id));
                }
            }

            entity.Content = viewModel.Content;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }
#endregion
    }
}
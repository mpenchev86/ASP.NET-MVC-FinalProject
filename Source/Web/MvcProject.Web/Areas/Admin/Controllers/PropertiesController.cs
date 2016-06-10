namespace MvcProject.Web.Areas.Admin.Controllers
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
    using GlobalConstants;
    using Infrastructure.Caching;
    using Infrastructure.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Descriptions;
    using ViewModels.Properties;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    //[NoCache]
    //[OutputCache(Duration = 0, NoStore = true, VaryByParam = "None")]
    public class PropertiesController : BaseGridController<Property, PropertyViewModel, IPropertiesService>
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

        public ActionResult Index()
        {
            var foreignKeys = new PropertyViewModelForeignKeys
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
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Property
                {
                    //Name = viewModel.Name,
                    //Value = viewModel.Value,
                    //DescriptionId = viewModel.DescriptionId,
                    //CreatedOn = viewModel.CreatedOn,
                    //ModifiedOn = viewModel.ModifiedOn,
                    //IsDeleted = viewModel.IsDeleted,
                    //DeletedOn = viewModel.DeletedOn
                };

                this.MapEntity(entity, viewModel);
                this.propertiesService.Insert(entity);
                viewModel.Id = entity.Id;
            }

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, PropertyViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Property
                {
                    Id = viewModel.Id,
                    //Name = viewModel.Name,
                    //Value = viewModel.Value,
                    //DescriptionId = viewModel.DescriptionId,
                    //CreatedOn = viewModel.CreatedOn,
                    //ModifiedOn = viewModel.ModifiedOn,
                    //IsDeleted = viewModel.IsDeleted,
                    //DeletedOn = viewModel.DeletedOn
                };

                this.MapEntity(entity, viewModel);
                this.propertiesService.Update(entity);
            }

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PropertyViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    this.propertiesService.DeletePermanent(viewModel.Id);
            //}

            return base.Destroy(request, viewModel);
        }

        public override void MapEntity(Property entity, PropertyViewModel viewModel)
        {
            entity.Name = viewModel.Name;
            entity.Value = viewModel.Value;
            entity.DescriptionId = viewModel.DescriptionId;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

#region PropertyDetailsHelpers
        public JsonResult GetAllPropertyDetailsForDescriptionViewModel()
        {
            var properties = this.propertiesService.GetAll().To<PropertyDetailsForDescriptionViewModel>();
            return this.Json(properties, JsonRequestBehavior.AllowGet);
        }
#endregion
    }
}
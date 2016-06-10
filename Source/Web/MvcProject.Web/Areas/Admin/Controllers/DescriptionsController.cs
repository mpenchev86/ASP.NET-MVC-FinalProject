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
    public class DescriptionsController : BaseGridController<Description, DescriptionViewModel, IDescriptionsService>
    {
        private readonly IDescriptionsService descriptionsService;

        public DescriptionsController(IDescriptionsService descriptionsService)
            : base(descriptionsService)
        {
            this.descriptionsService = descriptionsService;
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
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Save record to base
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, DescriptionViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Edit record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, DescriptionViewModel viewModel)
        {
            //if (viewModel != null)
            //{
            //    // Destroy record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Destroy(request, viewModel);
        }

        //[HttpPost]
        //public ActionResult GetPropertiesByDescriptionId([DataSourceRequest]DataSourceRequest request, int? descriptionId)
        //{
        //    var properties = new List<PropertyDetailsForDescriptionViewModel>();
        //    if (descriptionId != null)
        //    {
        //        properties = this.descriptionsService
        //            .GetById((int)descriptionId)
        //            .Properties
        //            .AsQueryable()
        //            .To<PropertyDetailsForDescriptionViewModel>()
        //            .ToList()
        //            ;
        //    }

        //    //return this.GetCollectionAsDataSourceResult(request, properties, this.ModelState);
        //    return this.Json(properties.AsEnumerable().ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        //}

        //public override IEnumerable<DescriptionViewModel> GetDataAsEnumerable()
        //{
        //    return base.GetDataAsEnumerable().OrderBy(x => x.Id);
        //}
    }
}
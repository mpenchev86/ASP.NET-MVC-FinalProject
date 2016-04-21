namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models.EntityContracts;
    using GlobalConstants;
    using Infrastructure.Extensions;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MvcProject.Web.Areas.Common.Controllers;
    using Services.Data;
    using ViewModels.Products;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class BaseGridController</*TSourceModel, */TService, TDestModel> : BaseController
        //where TSourceModel : class, IAdministerable
        where TDestModel : IMapFrom<IAdministerable>
        where TService : IBaseService<IAdministerable>
    {
        private TService service;

        public BaseGridController(TService service)
        {
            this.service = service;
        }

        [HttpPost]
        public virtual ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.service.GetAll().To<TDestModel>();

            return this.Json(viewModel.ToDataSourceResult(request));
        }

        [HttpPost]
        public virtual ActionResult Create([DataSourceRequest]DataSourceRequest request, TDestModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                // Save record to base
                //var model = this.service.Post
            }

            return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public virtual ActionResult Update([DataSourceRequest]DataSourceRequest request, TDestModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                // Edit record
            }

            return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public virtual ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TDestModel viewModel)
        {
            if (viewModel != null)
            {
                // Destroy record
            }

            return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
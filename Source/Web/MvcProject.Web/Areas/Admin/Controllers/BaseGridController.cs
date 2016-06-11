namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models.EntityContracts;
    using GlobalConstants;
    using Infrastructure.Caching;
    using Infrastructure.Extensions;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MvcProject.Web.Areas.Common.Controllers;
    using Services.Data;
    using ViewModels;
    using ViewModels.Products;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    //[OutputCache(Duration = 0, NoStore = true, VaryByParam = "None")]
    //[NoCache]
    public class BaseGridController<TEntityModel, TViewModel, TService> : BaseController
        where TEntityModel : /*class*/ BaseEntityModel<int>, IAdministerable
        where TViewModel : /*class*/ BaseAdminViewModel, IMapFrom<TEntityModel>
        where TService : IBaseService<TEntityModel>
    {
        private TService dataService;

        public BaseGridController(TService service)
        {
            this.dataService = service;
        }

        [HttpPost]
        public virtual ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.GetDataAsEnumerable().ToList();
            var dataSourceResult = viewModel.ToDataSourceResult(request, this.ModelState);
            return this.Json(dataSourceResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult Create([DataSourceRequest]DataSourceRequest request, TViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Save record to base
            //    //var model = this.service.Post
            //}

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

        [HttpPost]
        public virtual ActionResult Update([DataSourceRequest]DataSourceRequest request, TViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Edit record
            //}

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

        [HttpPost]
        public virtual ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                //var entity = this.dataService.GetById();
                this.dataService.DeletePermanent(viewModel.Id);
            }

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

        public virtual void MapEntity(TEntityModel entity, TViewModel viewModel)
        {
        }

#region DataProviders
        public virtual IEnumerable<TViewModel> GetDataAsEnumerable()
        {
            return this.dataService.GetAll().To<TViewModel>();
        }

        public virtual JsonResult GetDataAsJson()
        {
            return this.Json(this.GetDataAsEnumerable(), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetEntityAsDataSourceResult<T>([DataSourceRequest]DataSourceRequest request, T data, ModelStateDictionary modelState)
        {
            return this.Json(new[] { data }.ToDataSourceResult(request, modelState), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetCollectionAsDataSourceResult<T>([DataSourceRequest]DataSourceRequest request, IEnumerable<T> data, ModelStateDictionary modelState)
        {
            return this.Json(data.ToDataSourceResult(request, modelState), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetAsSelectList(string text, string value)
        {
            var data = this.GetDataAsEnumerable().Select(x => new SelectListItem { Text = text, Value = value});
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }
#endregion
    }
}
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
    public class BaseGridController<TEntityModel, TViewModel, TService, TKey> : BaseController
        where TEntityModel : class, IBaseEntityModel<TKey>, IAdministerable
        where TViewModel : /*class */BaseAdminViewModel<TKey>, IMapFrom<TEntityModel>
        where TService : IBaseService<TEntityModel, TKey>
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
            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

        [HttpPost]
        public virtual ActionResult Update([DataSourceRequest]DataSourceRequest request, TViewModel viewModel)
        {
            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

        [HttpPost]
        public virtual ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TViewModel viewModel/*, int viewModelId*/)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                this.dataService.DeletePermanent(viewModel.Id);
            }

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

#region DataProviders
        protected virtual void PopulateEntity(TEntityModel entity, TViewModel viewModel)
        {
        }

        protected virtual IEnumerable<TViewModel> GetDataAsEnumerable()
        {
            return this.dataService.GetAll().To<TViewModel>();
        }

        protected virtual JsonResult GetDataAsJson()
        {
            return this.Json(this.GetDataAsEnumerable(), JsonRequestBehavior.AllowGet);
        }

        protected virtual JsonResult GetEntityAsDataSourceResult<T>([DataSourceRequest]DataSourceRequest request, T data, ModelStateDictionary modelState)
        {
            return this.Json(new[] { data }.ToDataSourceResult(request, modelState), JsonRequestBehavior.AllowGet);
        }

        protected virtual JsonResult GetCollectionAsDataSourceResult<T>([DataSourceRequest]DataSourceRequest request, IEnumerable<T> data, ModelStateDictionary modelState)
        {
            return this.Json(data.ToDataSourceResult(request, modelState), JsonRequestBehavior.AllowGet);
        }

        protected virtual JsonResult GetAsSelectList(string text, string value)
        {
            var data = this.GetDataAsEnumerable().Select(x => new SelectListItem { Text = text, Value = value });
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }
#endregion
    }
}
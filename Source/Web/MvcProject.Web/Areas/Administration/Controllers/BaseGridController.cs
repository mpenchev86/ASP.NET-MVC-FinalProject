namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models.Contracts;
    using MvcProject.Common.GlobalConstants;
    using Infrastructure.Caching;
    using Infrastructure.Extensions;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels;
    using ViewModels.Products;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class BaseGridController<TEntityModel, TViewModel, TService, TKey> : BaseAdminController
        where TEntityModel : class, IBaseEntityModel<TKey>, IAdministerable, new()
        where TViewModel : BaseAdminViewModel<TKey>, IMapFrom<TEntityModel>
        where TService : IBaseDataService<TEntityModel, TKey>
    {
        private TService dataService;

        public BaseGridController(TService service)
        {
            this.dataService = service;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.GetDataAsEnumerable().ToList();
            var dataSourceResult = viewModel.ToDataSourceResult(request, this.ModelState);
            return this.Json(dataSourceResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([DataSourceRequest]DataSourceRequest request, TViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new TEntityModel();
                this.PopulateEntity(entity, viewModel);
                this.dataService.Insert(entity);
                viewModel.Id = entity.Id;
            }

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Update([DataSourceRequest]DataSourceRequest request, TViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = this.dataService.GetById(viewModel.Id);
                if (entity != null)
                {
                    this.PopulateEntity(entity, viewModel);
                    this.dataService.Update(entity);
                }
            }

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                this.HandleDependentEntitiesBeforeDelete(viewModel);
                this.dataService.DeletePermanent(viewModel.Id);
            }

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

        #region DataProviders
        protected virtual JsonResult GetDataAsJson()
        {
            return this.Json(this.GetDataAsEnumerable(), JsonRequestBehavior.AllowGet);
        }

        protected virtual IEnumerable<TViewModel> GetDataAsEnumerable()
        {
            var result = this.dataService.GetAll().To<TViewModel>().OrderBy(vm => vm.Id);
            return result;
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

        // TODO: Separate create from edit logic?
        /// <summary>
        /// Used to populate data to a new entity or repopulate an existing one.
        /// </summary>
        /// <param name="entity">The domain model</param>
        /// <param name="viewModel">The viewmodel</param>
        /// <param name="additionalParams">additional parameters array</param>
        protected virtual void PopulateEntity(TEntityModel entity, TViewModel viewModel, params object[] additionalParams)
        {
        }

        /// <summary>
        /// Prevents conflicts when deleting an entity which is being reffered to by other entities' navigation properties.
        /// </summary>
        /// <param name="viewModel">The entity's ViewModel used to manipulate data before persisting the changes.</param>
        protected virtual void HandleDependentEntitiesBeforeDelete(TViewModel viewModel)
        {
        }
        #endregion
    }
}
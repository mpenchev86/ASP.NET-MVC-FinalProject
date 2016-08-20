namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Roles;
    using ViewModels.Users;

    public class RolesController : BaseGridController<ApplicationRole, RoleViewModel, IRolesService, string>
    {
        private readonly IRolesService rolesService;
        private readonly IUsersService usersService;

        public RolesController(
            IRolesService rolesService,
            IUsersService usersService)
            : base(rolesService)
        {
            this.rolesService = rolesService;
            this.usersService = usersService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var foreignKeys = new RoleViewModelForeignKeys
            {
                Users = this.usersService.GetAll().To<UserDetailsForRoleViewModel>()
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
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, RoleViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, RoleViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, RoleViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region DataProviders
        protected override void PopulateEntity(ApplicationRole entity, RoleViewModel viewModel, params object[] additionalParams)
        {
            entity.Name = viewModel.Name;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }
        #endregion
    }
}
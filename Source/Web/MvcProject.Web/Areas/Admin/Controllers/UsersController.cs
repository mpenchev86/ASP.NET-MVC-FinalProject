namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Common.Controllers;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using MvcProject.Services.Data;
    using ViewModels;
    using ViewModels.Comments;
    using ViewModels.Roles;
    using ViewModels.Users;
    using ViewModels.Votes;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class UsersController : BaseGridController<ApplicationUser, UserViewModel, IUsersService, string>
    {
        private readonly IUsersService usersService;
        private readonly IRolesService rolesService;
        private readonly IUserRolesService<ApplicationUserRole> userRolesService;
        private readonly ICommentsService commentsService;
        private readonly IVotesService votesService;

        public UsersController(
            IUsersService usersService,
            IRolesService rolesService,
            IUserRolesService<ApplicationUserRole> userRolesService,
            ICommentsService commentsService,
            IVotesService votesService)
            : base(usersService)
        {
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.userRolesService = userRolesService;
            this.commentsService = commentsService;
            this.votesService = votesService;
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            var foreignKeys = new UserViewModelForeignKeys
            {
                Roles = this.rolesService.GetAll().To<RoleDetailsForUserViewModel>().ToList()
            };

            return this.View(foreignKeys);
        }

        [HttpGet]
        public ActionResult TestRemoveFromRole()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult TestRemoveFromRole(string removeId, string addId)
        {
            this.usersService.RemoveFromRoles(removeId, new string[] { "test" });
            this.usersService.AddToRoles(addId, new string[] { "Customer" });
            return this.View();
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        // Implemented through ASP.NET Identity user manager
        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new ApplicationUser { Id = viewModel.Id };
                this.PopulateEntity(entity, viewModel);
                this.usersService.Update(entity);
            }

            return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region DataProviders
        public JsonResult GetAllRoles()
        {
            var roles = this.rolesService.GetAll().To<RoleDetailsForUserViewModel>();
            return this.Json(roles, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRolesForUserViewModel(string id)
        {
            // Missing map from ApplicationUserRole to RoleDetailsForUserViewModel
            var roles = this.usersService.GetById(id).Roles.Select(r => new RoleDetailsForUserViewModel { Id = r.RoleId, Name = r.RoleName })/*AsQueryable().To<RoleDetailsForUserViewModel>()*/;
            return this.Json(roles, JsonRequestBehavior.AllowGet);
        }

        protected override void PopulateEntity(ApplicationUser entity, UserViewModel viewModel)
        {
            if (viewModel.Comments != null)
            {
                entity.Comments = new List<Comment>();
                foreach (var comment in viewModel.Comments)
                {
                    entity.Comments.Add(this.commentsService.GetById(comment.Id));
                }
            }

            if (viewModel.Votes != null)
            {
                entity.Votes = new List<Vote>();
                foreach (var vote in viewModel.Votes)
                {
                    entity.Votes.Add(this.votesService.GetById(vote.Id));
                }
            }

            //using (var userManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>())
            //{
            //}

            //var entityRoles = this.usersService.GetUserRoles(entity.Id).ToArray();
            //this.usersService.RemoveFromRoles(entity.Id, entityRoles);
            //var viewRoles = viewModel.Roles.Select(r => r.Name).ToArray();
            //this.usersService.AddToRoles(viewModel.Id, viewRoles);

            this.ProcessUserRoles(entity.Roles, viewModel.Roles, viewModel.Id, viewModel.UserName);

            entity.UserName = viewModel.UserName;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        private void ProcessUserRoles(ICollection<ApplicationUserRole> entityRoles, ICollection<RoleDetailsForUserViewModel> viewModelRoles, string userId, string userName)
        {
            //var entityRolesNames = entityRoles.Select(r => r.RoleName).ToList();
            var viewModelRolesNames = viewModelRoles.Select(r => r.Name).ToList();

            //foreach (var roleName in entityRolesNames)
            //{
            //    if (viewModelRolesNames.Contains(roleName))
            //    {
            //        viewModelRolesNames.Remove(roleName);
            //    }
            //    else
            //    {
            //        this.userRolesService.RemoveUserFromAllRoles(userName);
            //    }
            //}

            this.userRolesService.RemoveUserFromAllRoles(userName);

            foreach (var role in viewModelRolesNames)
            {
                this.userRolesService.AddUserToRole(new ApplicationUserRole
                {
                    UserId = userId,
                    UserName = userName,
                    RoleId = this.rolesService.GetByName(role).Id,
                    RoleName = role
                });
            }
        }
        #endregion
    }
}
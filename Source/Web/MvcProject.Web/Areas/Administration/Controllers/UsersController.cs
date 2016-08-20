namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using MvcProject.Common.GlobalConstants;
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

    [Authorize(Roles = IdentityRoles.Admin)]
    public class UsersController : BaseGridController<ApplicationUser, UserViewModel, IUsersService, string>
    {
        private readonly IUsersService usersService;
        private readonly IRolesService rolesService;
        private readonly IUserRolesService<ApplicationUserRole> userRolesService;
        private readonly ICommentsService commentsService;
        private readonly IVotesService votesService;
        private readonly IProductsService productsService;

        public UsersController(
            IUsersService usersService,
            IRolesService rolesService,
            IUserRolesService<ApplicationUserRole> userRolesService,
            ICommentsService commentsService,
            IVotesService votesService,
            IProductsService productsService)
            : base(usersService)
        {
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.userRolesService = userRolesService;
            this.commentsService = commentsService;
            this.votesService = votesService;
            this.productsService = productsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var foreignKeys = new UserViewModelForeignKeys
            {
                Roles = this.rolesService.GetAll().To<RoleDetailsForUserViewModel>()
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        // Implemented through ASP.NET Identity user manager. Currently, users are created by the registration form
        // of the AccountController and the grid Create toolbar button in the view is disabled
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region DataProviders
        [HttpGet]
        public JsonResult GetAllRoles()
        {
            var roles = this.rolesService.GetAll().To<RoleDetailsForUserViewModel>();
            return this.Json(roles, JsonRequestBehavior.AllowGet);
        }

        protected override void PopulateEntity(ApplicationUser entity, UserViewModel viewModel, params object[] additionalParams)
        {
            if (viewModel.Comments != null)
            {
                foreach (var comment in viewModel.Comments)
                {
                    entity.Comments.Add(this.commentsService.GetById(comment.Id));
                }
            }

            if (viewModel.Votes != null)
            {
                foreach (var vote in viewModel.Votes)
                {
                    entity.Votes.Add(this.votesService.GetById(vote.Id));
                }
            }

            var viewModelRoleNames = viewModel.Roles.Select(r => r.Name).ToList();
            this.ProcessUserRoles(viewModelRoleNames, viewModel.Id, viewModel.UserName);

            entity.UserName = viewModel.UserName;
            entity.Email = viewModel.Email;
            entity.EmailConfirmed = viewModel.EmailConfirmed;
            entity.PasswordHash = viewModel.PasswordHash;
            entity.AccessFailedCount = viewModel.AccessFailedCount;
            entity.SecurityStamp = viewModel.SecurityStamp;
            entity.PhoneNumber = viewModel.PhoneNumber;
            entity.PhoneNumberConfirmed = viewModel.PhoneNumberConfirmed;
            entity.TwoFactorEnabled = viewModel.TwoFactorEnabled;
            entity.LockoutEndDateUtc = viewModel.LockoutEndDateUtc;
            entity.LockoutEnabled = viewModel.LockoutEnabled;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        private void ProcessUserRoles(ICollection<string> viewModelRoleNames, string userId, string userName)
        {
            //// TODO: clear product seller from products when user is removed from seller role
            //if (this.userRolesService.GetByUserId(userId).Select(x => x.RoleName).Contains(IdentityRoles.Seller) &&
            //    !viewModelRoleNames.Contains(IdentityRoles.Seller))
            //{
            //    var products = this.productsService
            //        .GetAll()
            //        .Where(x => x.SellerId == userId);
            //    foreach (var product in products)
            //    {
            //        product.SellerId = null;
            //        this.productsService.Update(product);
            //    }
            //}

            this.userRolesService.RemoveUserFromAllRoles(userName);
            foreach (var role in viewModelRoleNames)
            {
                this.userRolesService.CreateUserRole(new ApplicationUserRole
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
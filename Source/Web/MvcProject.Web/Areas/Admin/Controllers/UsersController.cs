namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common.Controllers;
    using Microsoft.AspNet.Identity.Owin;
    using MvcProject.Services.Data;
    using MvcProject.Data.Common.Constants;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Data.Models;
    using Infrastructure.Extensions;
    using ViewModels;
    using ViewModels.Users;

    //[Authorize(Roles = Data.Common.Constants.IdentityRoleTypes.Administrator)]
    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            var model = this.usersService.GetAll().To<IndexUserViewModel>().ToList();
            //var userManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var model = userManager.Users.To<IndexUserViewModel>().ToList();
            return this.View(model);
        }

        public ActionResult GetUser(string id)
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult EditUser(PostUserViewModel model)
        {
            return null;
        }

        [HttpPost]
        public ActionResult DeleteUser(PostUserViewModel model)
        {
            return null;
        }

    }
}
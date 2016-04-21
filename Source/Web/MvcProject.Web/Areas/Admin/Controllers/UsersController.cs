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
    using Infrastructure.Extensions;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using MvcProject.GlobalConstants;
    using MvcProject.Services.Data;
    using ViewModels;
    using ViewModels.Users;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
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
            //var userManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var model = this.usersService.GetAll().To<UserViewModel>().ToList();

            return this.View(model);
        }

        public ActionResult GetUser(string id)
        {
            var model = this.Mapper.Map<UserViewModel>(this.usersService.GetById(id));
            model.MainRole = this.usersService.GetUserRoles(model.Id).FirstOrDefault();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserPostModel model)
        {
            return null;
        }

        [HttpPost]
        public ActionResult DeleteUser(UserPostModel model)
        {
            return null;
        }

    }
}
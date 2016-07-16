namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using Services.Data;
    using ViewModels.Users;

    public class UsersController : BasePublicController
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [DisplayName("Profile")]
        public ActionResult UserProfile(string userId)
        {
            var user = this.usersService.GetById(userId);
            var result = this.Mapper.Map<ApplicationUser, UserProfileViewModel>(user);
            return this.View(result);
        }
    }
}
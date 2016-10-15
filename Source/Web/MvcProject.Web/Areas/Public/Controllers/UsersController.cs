namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Services.Data;
    using ViewModels.Users;

    public class UsersController : BasePublicController
    {
        private IUsersService usersService;
        //private IMappingService

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public ActionResult UserProfile(string userName)
        {
            var user = this.usersService.GetByUserName(userName);
            var result = this.Mapper.Map<ApplicationUser, UserProfileViewModel>(user);
            return this.View(result);
        }
    }
}
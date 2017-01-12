namespace JustOrderIt.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Identity;
    using Infrastructure.Mapping;
    using Services.Data;
    using ViewModels.Users;

    public class UsersController : BasePublicController
    {
        private readonly IUsersService usersService;
        private readonly IMappingService mappingService;

        public UsersController(IUsersService usersService, IMappingService mappingService)
        {
            this.usersService = usersService;
            this.mappingService = mappingService;
        }

        public ActionResult UserProfile(string userName)
        {
            var user = this.usersService.GetByUserName(userName);
            var result = this.mappingService.Map<ApplicationUser, UserProfileViewModel>(user);
            return this.View(result);
        }
    }
}
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

        [Authorize]
        public ActionResult UserHomePage()
        {
            return this.View();
        }

        [Authorize]
        public ActionResult WishList()
        {
            return this.PartialView("UnderConstruction", null);
        }

        [Authorize]
        public ActionResult OrderHistory()
        {
            return this.PartialView("UnderConstruction", null);
        }

        [Authorize]
        public ActionResult UserComments()
        {
            return this.PartialView("UnderConstruction", null);
        }

        public ActionResult PublicUserProfile(string userName)
        {
            var user = this.usersService.GetByUserName(userName);
            var viewModel = this.mappingService.Map<ApplicationUser, PublicUserProfile>(user);
            return this.View(viewModel);
        }
    }
}
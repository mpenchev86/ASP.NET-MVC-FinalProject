namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common.GlobalConstants;
    using Data.Models;
    using Services.Data;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class SellersController : UsersController
    {
        public SellersController(
            IUsersService usersService,
            IRolesService rolesService,
            IUserRolesService<ApplicationUserRole> userRolesService,
            ICommentsService commentsService,
            IVotesService votesService,
            IProductsService productsService) 
            : base(usersService, rolesService, userRolesService, commentsService, votesService, productsService)
        {
        }
    }
}
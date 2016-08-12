namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using Web;

    public class SellersService : UsersService, ISellersService
    {
        public SellersService(
            IStringPKDeletableRepository<ApplicationUser> users,
            IIdentifierProvider idProvider,
            UserManager<ApplicationUser, string> userManager)
            : base(users, idProvider, userManager)
        {
        }
    }
}

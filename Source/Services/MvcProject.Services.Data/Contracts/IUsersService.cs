namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MvcProject.Data.Models;

    public interface IUsersService : IBaseService<ApplicationUser>
    {
        IQueryable<string> GetUserRoles(string userId);

        IdentityResult RemoveFromRoles(string userId, string[] roles);

        IdentityResult AddToRole(string userId, string[] roles);

        void DeleteUser(string userId);

        IdentityResult DeleteUserPermanent(string userId);
    }
}

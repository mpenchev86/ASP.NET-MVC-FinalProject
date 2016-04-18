namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using MvcProject.Data.DbAccessConfig;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return this.userManager.Users;
        }

        public ApplicationUser GetUserById(string id)
        {
            var result = this.userManager
                .Users
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return result;
        }

        public IQueryable<string> GetUserRoles(string userId)
        {
            var roles = this.userManager.GetRoles(userId).AsQueryable();
            return roles;
        }

        public IdentityResult RemoveFromRoles(string userId, string[] roles)
        {
            return this.userManager.RemoveFromRoles(userId, roles);
        }

        public IdentityResult AddToRole(string userId, string[] roles)
        {
            return this.userManager.AddToRoles(userId, roles);
        }
    }
}

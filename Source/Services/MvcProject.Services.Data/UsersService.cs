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
        //private readonly GenericRepository<ApplicationUser> users;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return this.userManager.Users;
        }

        public IQueryable<ApplicationUser> GetAllNotDeleted()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(string id)
        {
            var result = this.userManager
                .Users
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return result;
        }

        public ApplicationUser GetByIdFromAll(int id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetByIdFromAll(string id)
        {
            throw new NotImplementedException();
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

        public void DeleteUser(string userId)
        {
            var user = this.GetById(userId);
            user.IsDeleted = true;
            user.DeletedOn = DateTime.UtcNow;
        }

        public IdentityResult DeleteUserPermanent(string userId)
        {
            var user = this.GetById(userId);
            return this.userManager.Delete(user);
        }

        public void Insert(ApplicationUser propertyEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser propertyEntity)
        {
            throw new NotImplementedException();
        }

        public void MarkAsDeleted(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletePermanent(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletePermanent(ApplicationUser propertyEntity)
        {
            throw new NotImplementedException();
        }
    }
}

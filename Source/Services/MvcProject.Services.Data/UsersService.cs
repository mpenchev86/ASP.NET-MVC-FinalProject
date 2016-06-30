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
    using Web;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser, string> userManager;
        private readonly RoleManager<ApplicationRole, string> roleManager;
        private readonly IStringPKRepositoryDeletable<ApplicationUser> users;
        private IIdentifierProvider idProvider;

        public UsersService(
            UserManager<ApplicationUser, string> userManager,
            RoleManager<ApplicationRole, string> roleManager,
            IStringPKRepositoryDeletable<ApplicationUser> users,
            IIdentifierProvider idProvider)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.users = users;
            this.idProvider = idProvider;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            // var users = this.userManager.Users;
            // var asList = users.ToList();
            // return users;
            var result = this.users.All().OrderBy(x => x.Id);
            return result;
        }

        public IQueryable<ApplicationUser> GetAllNotDeleted()
        {
            // return this.userManager.Users.Where(user => !user.IsDeleted);
            var result = this.users.AllNotDeleted().OrderBy(x => x.Id);
            return result;
        }

        public ApplicationUser GetById(string id)
        {
            // var result = this.GetAll()
            //    .Where(x => x.Id == id)
            //    .FirstOrDefault();
            // return result;
            return this.users.GetById(id);
        }

        public ApplicationUser GetByEncodedId(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var user = this.users.GetById(decodedId);
            return user;
        }

        public ApplicationUser GetByIdFromNotDeleted(string id)
        {
            // var result = this.GetAllNotDeleted()
            //    .Where(x => x.Id == id)
            //    .FirstOrDefault();
            // return result;
            return this.users.GetByIdFromNotDeleted(id);
        }

        public ApplicationUser GetByEncodedIdFromNotDeleted(string id)
        {
            // var decodedId = this.idProvider.DecodeIdToString(id);
            // var result = this.GetAllNotDeleted()
            //    .Where(x => x.Id == decodedId)
            //    .FirstOrDefault();
            // return result;
            var decodedId = this.idProvider.DecodeIdToString(id);
            var user = this.users.GetByIdFromNotDeleted(decodedId);
            return user;
        }

        public IQueryable<ApplicationRole> GetAllRoles()
        {
            var roles = this.roleManager.Roles.OrderBy(r => r.Name);
            return roles;
        }

        public IQueryable<string> GetUserRoles(string userId)
        {
            var roles = this.userManager.GetRoles(userId).AsQueryable();
            return roles;
        }

        public async Task<IdentityResult> AddToRoles(string userId, string[] roles)
        {
            var result = await this.userManager.AddToRolesAsync(userId, roles);
            return result;
        }

        public async Task<IdentityResult> RemoveFromRole(string userId, string role)
        {
            var result = await this.userManager.RemoveFromRoleAsync(userId, role);
            return result;
        }

        public async Task<IdentityResult> RemoveFromRoles(string userId, string[] roles)
        {
            IdentityResult result = new IdentityResult();
            foreach (var role in roles)
            {
                result = await this.RemoveFromRole(userId, role);
                if (!result.Succeeded)
                {
                    return result;
                }
            }

            return result;
        }

        // Not used, AccountController has Register method implemented with userManager
        public void Insert(ApplicationUser entity)
        {
            // throw new NotImplementedException();
            this.users.Add(entity);
            this.users.SaveChanges();
        }

        public void Update(ApplicationUser entity)
        {
            // this.ApplyMarkAsDeleted(entity);
            // this.userManager.Update(entity);
            this.users.Update(entity);
            this.users.SaveChanges();
        }

        public void MarkAsDeleted(string id)
        {
            // var entity = this.GetById(id);
            // this.MarkAsDeleted(entity);
            // this.userManager.Update(entity);
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.users.SaveChanges();
        }

        public void MarkAsDeleted(ApplicationUser entity)
        {
            // entity.DeletedOn = DateTime.Now;
            // this.userManager.Update(entity);
            entity.IsDeleted = true;
            this.users.SaveChanges();
        }

        public void DeletePermanent(string id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.users.SaveChanges();
        }

        public void DeletePermanent(ApplicationUser entity)
        {
            // this.userManager.Delete(entity);
            this.users.DeletePermanent(entity);
            this.users.SaveChanges();
        }
    }
}

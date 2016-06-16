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
        private readonly UserManager<ApplicationUser> userManager;
        private IIdentifierProvider idProvider;

        public UsersService(UserManager<ApplicationUser> userManager, IIdentifierProvider idProvider)
        {
            this.userManager = userManager;
            this.idProvider = idProvider;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            var users = this.userManager.Users;
            var asList = users.ToList();
            return users;
        }

        public IQueryable<ApplicationUser> GetAllNotDeleted()
        {
            return this.userManager.Users.Where(user => !user.IsDeleted);
        }

        public ApplicationUser GetById(string id)
        {
            var result = this.GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return result;
        }

        public ApplicationUser GetByEncodedId(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var user = this.GetById(decodedId);
            return user;
        }

        public ApplicationUser GetByIdFromNotDeleted(string id)
        {
            var result = this.GetAllNotDeleted()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return result;
        }

        public ApplicationUser GetByEncodedIdFromNotDeleted(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var result = this.GetAllNotDeleted()
                .Where(x => x.Id == decodedId)
                .FirstOrDefault();
            return result;
        }

        public IQueryable<string> GetUserRoles(string userId)
        {
            var roles = this.userManager.GetRoles(userId).AsQueryable();
            return roles;
        }

        public IdentityResult AddToRole(string userId, string[] roles)
        {
            return this.userManager.AddToRoles(userId, roles);
        }

        public IdentityResult RemoveFromRoles(string userId, string[] roles)
        {
            return this.userManager.RemoveFromRoles(userId, roles);
        }

        public void Insert(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser entity)
        {
            this.ApplyMarkAsDeleted(entity);
            this.userManager.Update(entity);
        }

        public void MarkAsDeleted(int id)
        {
            throw new NotImplementedException();
        }

        public void MarkAsDeleted(string id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.userManager.Update(entity);
        }

        public void MarkAsDeleted(ApplicationUser entity)
        {
            entity.DeletedOn = DateTime.Now;
            this.userManager.Update(entity);
        }

        public void DeletePermanent(string id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
        }

        public void DeletePermanent(ApplicationUser entity)
        {
            this.userManager.Delete(entity);
        }

        //public IdentityResult DeleteUserPermanent(string userId)
        //{
        //    var user = this.GetById(userId);
        //    return this.userManager.Delete(user);
        //}

        private void ApplyMarkAsDeleted(ApplicationUser entity)
        {
            if (entity.IsDeleted)
            {
                this.MarkAsDeleted(entity);
            }
        }
    }
}

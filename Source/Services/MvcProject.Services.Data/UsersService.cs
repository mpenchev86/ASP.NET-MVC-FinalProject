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
        private readonly IStringPKRepositoryDeletable<ApplicationUser> users;
        private IIdentifierProvider idProvider;

        public UsersService(
            IStringPKRepositoryDeletable<ApplicationUser> users,
            IIdentifierProvider idProvider)
        {
            this.users = users;
            this.idProvider = idProvider;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            var result = this.users.All().OrderBy(x => x.Id);
            return result;
        }

        public IQueryable<ApplicationUser> GetAllNotDeleted()
        {
            var result = this.users.AllNotDeleted().OrderBy(x => x.Id);
            return result;
        }

        public ApplicationUser GetById(string id)
        {
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
            return this.users.GetByIdFromNotDeleted(id);
        }

        public ApplicationUser GetByEncodedIdFromNotDeleted(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var user = this.users.GetByIdFromNotDeleted(decodedId);
            return user;
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
            this.users.Update(entity);
            this.users.SaveChanges();
        }

        public void MarkAsDeleted(string id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.users.SaveChanges();
        }

        public void MarkAsDeleted(ApplicationUser entity)
        {
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
            this.users.DeletePermanent(entity);
            this.users.SaveChanges();
        }
    }
}

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

    public class RolesService : IRolesService
    {
        private readonly IStringPKRepositoryDeletable<ApplicationRole> roles;
        private readonly RoleManager<ApplicationRole, string> roleManager;
        private IIdentifierProvider idProvider;

        public RolesService(
            IStringPKRepositoryDeletable<ApplicationRole> roles,
            RoleManager<ApplicationRole, string> roleManager,
            IIdentifierProvider idProvider)
        {
            this.roles = roles;
            this.roleManager = roleManager;
            this.idProvider = idProvider;
        }

        public IQueryable<ApplicationRole> GetAll()
        {
            var result = this.roles.All().OrderBy(x => x.Name);
            return result;
        }

        public IQueryable<ApplicationRole> GetAllNotDeleted()
        {
            var result = this.roles.AllNotDeleted().OrderBy(x => x.Name);
            return result;
        }

        public ApplicationRole GetById(string id)
        {
            return this.roles.GetById(id);
        }

        public ApplicationRole GetByName(string name)
        {
            return this.roleManager.FindByName(name);
        }

        public ApplicationRole GetByEncodedId(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var role = this.roles.GetById(decodedId);
            return role;
        }

        public ApplicationRole GetByIdFromNotDeleted(string id)
        {
            return this.roles.GetByIdFromNotDeleted(id);
        }

        public ApplicationRole GetByEncodedIdFromNotDeleted(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var role = this.roles.GetByIdFromNotDeleted(decodedId);
            return role;
        }

        public void Insert(ApplicationRole entity)
        {
            this.roles.Add(entity);
            this.roles.SaveChanges();
        }

        public void Update(ApplicationRole entity)
        {
            this.roles.Update(entity);
            this.roles.SaveChanges();
        }

        public void MarkAsDeleted(string id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.roles.SaveChanges();
        }

        public void MarkAsDeleted(ApplicationRole entity)
        {
            entity.IsDeleted = true;
            this.roles.SaveChanges();
        }

        public void DeletePermanent(string id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.roles.SaveChanges();
        }

        public void DeletePermanent(ApplicationRole entity)
        {
            this.roles.DeletePermanent(entity);
            this.roles.SaveChanges();
        }
    }
}

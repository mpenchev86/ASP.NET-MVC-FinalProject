namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using Web;

    public class RolesService : IRolesService
    {
        private readonly IStringPKRepository<ApplicationRole> roles;
        private IIdentifierProvider idProvider;

        public RolesService(
            IStringPKRepository<ApplicationRole> roles,
            IIdentifierProvider idProvider)
        {
            this.roles = roles;
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

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

    public class DescriptionsService : IDescriptionsService
    {
        private readonly IIntPKRepository<Description> descriptions;
        private IIdentifierProvider idProvider;

        public DescriptionsService(IIntPKRepository<Description> descriptions, IIdentifierProvider idProvider)
        {
            this.descriptions = descriptions;
            this.idProvider = idProvider;
        }

        public IQueryable<Description> GetAll()
        {
            var result = this.descriptions.All().OrderBy(x => x.Content);
            return result;
        }

        public IQueryable<Description> GetAllNotDeleted()
        {
            var result = this.descriptions.AllNotDeleted().OrderBy(x => x.Content);
            return result;
        }

        public Description GetById(int id)
        {
            return this.descriptions.GetById(id);
        }

        public Description GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var description = this.descriptions.GetById(idAsInt);
            return description;
        }

        public Description GetByIdFromNotDeleted(int id)
        {
            return this.descriptions.GetByIdFromNotDeleted(id);
        }

        public Description GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var description = this.descriptions.GetByIdFromNotDeleted(idAsInt);
            return description;
        }

        public void Insert(Description entity)
        {
            this.descriptions.Add(entity);
            this.descriptions.SaveChanges();
        }

        public void Update(Description entity)
        {
            this.descriptions.Update(entity);
            this.descriptions.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.descriptions.SaveChanges();
        }

        public void MarkAsDeleted(Description entity)
        {
            entity.IsDeleted = true;
            this.descriptions.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.descriptions.SaveChanges();
        }

        public void DeletePermanent(Description entity)
        {
            this.descriptions.DeletePermanent(entity);
            this.descriptions.SaveChanges();
        }
    }
}

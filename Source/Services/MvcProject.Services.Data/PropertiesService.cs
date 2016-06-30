namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Services.Web;
    using MvcProject.Web.Infrastructure.Mapping;

    public class PropertiesService : IPropertiesService
    {
        private readonly IIntPKRepositoryDeletable<Property> properties;
        private IIdentifierProvider idProvider;

        public PropertiesService(IIntPKRepositoryDeletable<Property> properties, IIdentifierProvider idProvider)
        {
            this.properties = properties;
            this.idProvider = idProvider;
        }

        public IQueryable<Property> GetAll()
        {
            var result = this.properties.All().OrderBy(x => x.Name);
            return result;
        }

        public IQueryable<Property> GetAllNotDeleted()
        {
            var result = this.properties.AllNotDeleted().OrderBy(x => x.Name);
            return result;
        }

        public Property GetById(int id)
        {
            return this.properties.GetById(id);
        }

        public Property GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var property = this.properties.GetById(idAsInt);
            return property;
        }

        public Property GetByIdFromNotDeleted(int id)
        {
            return this.properties.GetByIdFromNotDeleted(id);
        }

        public Property GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var property = this.properties.GetByIdFromNotDeleted(idAsInt);
            return property;
        }

        public void Insert(Property entity)
        {
            this.properties.Add(entity);
            this.properties.SaveChanges();
        }

        public void Update(Property entity)
        {
            this.properties.Update(entity);
            this.properties.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.properties.SaveChanges();
        }

        public void MarkAsDeleted(Property entity)
        {
            entity.IsDeleted = true;
            this.properties.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.properties.SaveChanges();
        }

        public void DeletePermanent(Property entity)
        {
            this.properties.DeletePermanent(entity);
            this.properties.SaveChanges();
        }
    }
}

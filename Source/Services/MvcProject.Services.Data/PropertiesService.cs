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
        private readonly IRepository<Property> properties;
        private IIdentifierProvider idProvider;

        public PropertiesService(IRepository<Property> properties, IIdentifierProvider idProvider)
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

        public Property GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var property = this.properties.GetById(idAsInt);
            return property;
        }

        public Property GetByIdFromAll(int id)
        {
            return this.properties.GetByIdFromAll(id);
        }

        public Property GetByIdFromAll(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var property = this.properties.GetByIdFromAll(idAsInt);
            return property;
        }

        public void Insert(Property propertyEntity)
        {
            this.properties.Add(propertyEntity);
            this.properties.SaveChanges();
        }

        public void Update(Property propertyEntity)
        {
            this.properties.Update(propertyEntity);
            this.properties.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            entity.IsDeleted = true;
            this.properties.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetByIdFromAll(id);
            this.DeletePermanent(entity);
            //this.properties.SaveChanges();
        }

        public void DeletePermanent(Property entity)
        {
            this.properties.DeletePermanent(entity);
            this.properties.SaveChanges();
        }
    }
}

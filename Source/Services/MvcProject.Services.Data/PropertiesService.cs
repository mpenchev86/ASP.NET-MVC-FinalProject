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
            var result = this.properties
                             .All()
                             .OrderBy(x => x.Name);
            return result;
        }

        public Property GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var property = this.properties.GetById(idAsInt);
            return property;
        }

        public Property GetById(int id)
        {
            return this.properties.GetById(id);
        }
    }
}

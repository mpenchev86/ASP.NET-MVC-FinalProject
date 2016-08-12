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

    public class PropertiesService : BaseDataService<Property, int, IIntPKDeletableRepository<Property>>, IPropertiesService
    {
        public PropertiesService(IIntPKDeletableRepository<Property> properties, IIdentifierProvider idProvider)
            : base(properties, idProvider)
        {
        }

        public override Property GetByEncodedId(string id)
        {
            var property = this.Repository.GetById(this.IdentifierProvider.DecodeIdToInt(id));
            return property;
        }

        public override Property GetByEncodedIdFromNotDeleted(string id)
        {
            var property = this.Repository.GetByIdFromNotDeleted(this.IdentifierProvider.DecodeIdToInt(id));
            return property;
        }
    }
}

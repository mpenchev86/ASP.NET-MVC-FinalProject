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
        private readonly IRepository<Description> descriptions;
        private IIdentifierProvider idProvider;

        public DescriptionsService(IRepository<Description> descriptions, IIdentifierProvider idProvider)
        {
            this.descriptions = descriptions;
            this.idProvider = idProvider;
        }

        public IQueryable<Description> GetAll()
        {
            var result = this.descriptions
                             .All()
                             .OrderBy(x => x.Content);
            return result;
        }

        public Description GetById(int id)
        {
            return this.descriptions.GetById(id);
        }

        public Description GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var description = this.descriptions.GetById(idAsInt);
            return description;
        }
    }
}

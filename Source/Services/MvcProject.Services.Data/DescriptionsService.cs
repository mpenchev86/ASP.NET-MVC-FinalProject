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

    public class DescriptionsService : BaseDataService<Description, int, IIntPKDeletableRepository<Description>>, IDescriptionsService
    {
        private readonly IIntPKDeletableRepository<Description> descriptionsRepository;
        private IIdentifierProvider identifierProvider;

        public DescriptionsService(IIntPKDeletableRepository<Description> descriptions, IIdentifierProvider idProvider)
            : base(descriptions, idProvider)
        {
            this.descriptionsRepository = descriptions;
            this.identifierProvider = idProvider;
        }

        public override Description GetByEncodedId(string id)
        {
            var description = this.descriptionsRepository.GetById(this.identifierProvider.DecodeIdToInt(id));
            return description;
        }

        public override Description GetByEncodedIdFromNotDeleted(string id)
        {
            var description = this.descriptionsRepository.GetByIdFromNotDeleted(this.identifierProvider.DecodeIdToInt(id));
            return description;
        }
    }
}

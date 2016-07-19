﻿namespace MvcProject.Services.Data
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
        private IIdentifierProvider idProvider;

        public DescriptionsService(IIntPKDeletableRepository<Description> descriptions, IIdentifierProvider idProvider)
            : base(descriptions, idProvider)
        {
            this.descriptionsRepository = descriptions;
            this.idProvider = idProvider;
        }

        public override Description GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var description = this.descriptionsRepository.GetById(idAsInt);
            return description;
        }

        public override Description GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var description = this.descriptionsRepository.GetByIdFromNotDeleted(idAsInt);
            return description;
        }
    }
}

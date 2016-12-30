﻿namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.Catalog;
    using Web;

    public class VotesService : BaseDataService<Vote, int, IIntPKDeletableRepository<Vote>>, IVotesService
    {
        public VotesService(IIntPKDeletableRepository<Vote> votes, IIdentifierProvider idProvider)
            : base(votes, idProvider)
        {
        }

        public override Vote GetByEncodedId(string id)
        {
            var vote = this.Repository.GetById((int)this.IdentifierProvider.DecodeIdToInt(id));
            return vote;
        }

        public override Vote GetByEncodedIdFromNotDeleted(string id)
        {
            var vote = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeIdToInt(id));
            return vote;
        }
    }
}

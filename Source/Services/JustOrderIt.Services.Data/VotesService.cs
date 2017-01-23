namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using JustOrderIt.Data.DbAccessConfig.Repositories;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Data.Models.Catalog;
    using Web;

    public class VotesService : BaseDataService<Vote, int, IIntPKDeletableRepository<Vote>>, IVotesService
    {
        public VotesService(IIntPKDeletableRepository<Vote> votes, IIdentifierProvider idProvider)
            : base(votes, idProvider)
        {
        }

        public override Vote GetByEncodedId(string id)
        {
            var vote = this.Repository.GetById((int)this.IdentifierProvider.DecodeToIntId(id));
            return vote;
        }

        public override Vote GetByEncodedIdFromNotDeleted(string id)
        {
            var vote = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeToIntId(id));
            return vote;
        }
    }
}

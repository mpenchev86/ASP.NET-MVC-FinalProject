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

    public class VotesService : BaseDataService<Vote, int, IIntPKDeletableRepository<Vote>>, IVotesService
    {
        private readonly IIntPKDeletableRepository<Vote> votes;
        private IIdentifierProvider idProvider;

        public VotesService(IIntPKDeletableRepository<Vote> votes, IIdentifierProvider idProvider)
            : base(votes, idProvider)
        {
            this.votes = votes;
            this.idProvider = idProvider;
        }

        public override Vote GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var vote = this.votes.GetById(idAsInt);
            return vote;
        }

        public override Vote GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var vote = this.votes.GetByIdFromNotDeleted(idAsInt);
            return vote;
        }
    }
}

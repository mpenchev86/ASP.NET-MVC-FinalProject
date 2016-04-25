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

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votes;
        private IIdentifierProvider idProvider;

        public VotesService(IRepository<Vote> votes, IIdentifierProvider idProvider)
        {
            this.votes = votes;
            this.idProvider = idProvider;
        }

        public IQueryable<Vote> GetAll()
        {
            var result = this.votes
                             .All()
                             .OrderBy(x => x.Id);
            return result;
        }

        public Vote GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var vote = this.votes.GetById(idAsInt);
            return vote;
        }

        public Vote GetById(int id)
        {
            return this.votes.GetById(id);
        }
    }
}

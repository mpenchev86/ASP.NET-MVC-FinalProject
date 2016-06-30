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
        private readonly IIntPKRepositoryDeletable<Vote> votes;
        private IIdentifierProvider idProvider;

        public VotesService(IIntPKRepositoryDeletable<Vote> votes, IIdentifierProvider idProvider)
        {
            this.votes = votes;
            this.idProvider = idProvider;
        }

        public IQueryable<Vote> GetAll()
        {
            var result = this.votes.All().OrderBy(x => x.Id);
            return result;
        }

        public IQueryable<Vote> GetAllNotDeleted()
        {
            var result = this.votes.AllNotDeleted().OrderBy(x => x.Id);
            return result;
        }

        public Vote GetById(int id)
        {
            return this.votes.GetById(id);
        }

        public Vote GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var vote = this.votes.GetById(idAsInt);
            return vote;
        }

        public Vote GetByIdFromNotDeleted(int id)
        {
            return this.votes.GetByIdFromNotDeleted(id);
        }

        public Vote GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var vote = this.votes.GetByIdFromNotDeleted(idAsInt);
            return vote;
        }

        public void Insert(Vote entity)
        {
            this.votes.Add(entity);
            this.votes.SaveChanges();
        }

        public void Update(Vote entity)
        {
            this.votes.Update(entity);
            this.votes.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.votes.SaveChanges();
        }

        public void MarkAsDeleted(Vote entity)
        {
            entity.IsDeleted = true;
            this.votes.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.votes.SaveChanges();
        }

        public void DeletePermanent(Vote entity)
        {
            this.votes.DeletePermanent(entity);
            this.votes.SaveChanges();
        }
    }
}

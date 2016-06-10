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

        public Vote GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var vote = this.votes.GetById(idAsInt);
            return vote;
        }

        public Vote GetByIdFromAll(int id)
        {
            return this.votes.GetByIdFromAll(id);
        }

        public Vote GetByIdFromAll(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var vote = this.votes.GetByIdFromAll(idAsInt);
            return vote;
        }

        public void Insert(Vote propertyEntity)
        {
            this.votes.Add(propertyEntity);
            this.votes.SaveChanges();
        }

        public void Update(Vote propertyEntity)
        {
            this.votes.Update(propertyEntity);
            this.votes.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            entity.IsDeleted = true;
            this.votes.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetByIdFromAll(id);
            this.DeletePermanent(entity);
        }

        public void DeletePermanent(Vote entity)
        {
            this.votes.DeletePermanent(entity);
            this.votes.SaveChanges();
        }
    }
}

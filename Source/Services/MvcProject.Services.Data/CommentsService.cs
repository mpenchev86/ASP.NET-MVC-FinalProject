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

    public class CommentsService : ICommentsService
    {
        private readonly IIntPKRepositoryDeletable<Comment> comments;
        private IIdentifierProvider idProvider;

        public CommentsService(IIntPKRepositoryDeletable<Comment> comments, IIdentifierProvider idProvider)
        {
            this.comments = comments;
            this.idProvider = idProvider;
        }

        public IQueryable<Comment> GetAll()
        {
            var result = this.comments.All().OrderBy(x => x.Content);
            return result;
        }

        public IQueryable<Comment> GetAllNotDeleted()
        {
            var result = this.comments.AllNotDeleted().OrderBy(x => x.Content);
            return result;
        }

        public Comment GetById(int id)
        {
            return this.comments.GetById(id);
        }

        public Comment GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var comment = this.comments.GetById(idAsInt);
            return comment;
        }

        public Comment GetByIdFromNotDeleted(int id)
        {
            return this.comments.GetByIdFromNotDeleted(id);
        }

        public Comment GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var comment = this.comments.GetByIdFromNotDeleted(idAsInt);
            return comment;
        }

        public void Insert(Comment entity)
        {
            this.comments.Add(entity);
            this.comments.SaveChanges();
        }

        public void Update(Comment entity)
        {
            this.comments.Update(entity);
            this.comments.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.comments.SaveChanges();
        }

        public void MarkAsDeleted(Comment entity)
        {
            entity.IsDeleted = true;
            this.comments.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.comments.SaveChanges();
        }

        public void DeletePermanent(Comment entity)
        {
            this.comments.DeletePermanent(entity);
            this.comments.SaveChanges();
        }
    }
}

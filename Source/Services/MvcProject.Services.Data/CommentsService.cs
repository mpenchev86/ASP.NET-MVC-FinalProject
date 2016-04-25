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
        private readonly IRepository<Comment> comments;
        private IIdentifierProvider idProvider;

        public CommentsService(IRepository<Comment> comments, IIdentifierProvider idProvider)
        {
            this.comments = comments;
            this.idProvider = idProvider;
        }

        public IQueryable<Comment> GetAll()
        {
            var result = this.comments
                             .All()
                             .OrderBy(x => x.Content);
            return result;
        }

        public Comment GetById(int id)
        {
            return this.comments.GetById(id);
        }

        public Comment GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var comment = this.comments.GetById(idAsInt);
            return comment;
        }
    }
}

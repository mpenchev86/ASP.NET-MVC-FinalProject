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

    public class CommentsService : BaseDataService<Comment, int, IIntPKDeletableRepository<Comment>>, ICommentsService
    {
        private readonly IIntPKDeletableRepository<Comment> commentsRepository;
        private IIdentifierProvider identifierProvider;

        public CommentsService(IIntPKDeletableRepository<Comment> comments, IIdentifierProvider idProvider)
            : base(comments, idProvider)
        {
            this.commentsRepository = comments;
            this.identifierProvider = idProvider;
        }

        public override Comment GetByEncodedId(string id)
        {
            var comment = this.commentsRepository.GetById(this.identifierProvider.DecodeIdToInt(id));
            return comment;
        }

        public override Comment GetByEncodedIdFromNotDeleted(string id)
        {
            var comment = this.commentsRepository.GetByIdFromNotDeleted(this.identifierProvider.DecodeIdToInt(id));
            return comment;
        }
    }
}

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
        private IIdentifierProvider idProvider;

        public CommentsService(IIntPKDeletableRepository<Comment> comments, IIdentifierProvider idProvider)
            : base(comments, idProvider)
        {
            this.commentsRepository = comments;
            this.idProvider = idProvider;
        }

        public override Comment GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var comment = this.commentsRepository.GetById(idAsInt);
            return comment;
        }

        public override Comment GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var comment = this.commentsRepository.GetByIdFromNotDeleted(idAsInt);
            return comment;
        }
    }
}

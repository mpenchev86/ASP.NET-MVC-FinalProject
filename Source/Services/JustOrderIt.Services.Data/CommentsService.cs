﻿namespace JustOrderIt.Services.Data
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

    public class CommentsService : BaseDataService<Comment, int, IIntPKDeletableRepository<Comment>>, ICommentsService
    {
        public CommentsService(IIntPKDeletableRepository<Comment> comments, IIdentifierProvider idProvider)
            : base(comments, idProvider)
        {
        }

        public override Comment GetByEncodedId(string id)
        {
            var comment = this.Repository.GetById((int)this.IdentifierProvider.DecodeToIntId(id));
            return comment;
        }

        public override Comment GetByEncodedIdFromNotDeleted(string id)
        {
            var comment = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeToIntId(id));
            return comment;
        }
    }
}

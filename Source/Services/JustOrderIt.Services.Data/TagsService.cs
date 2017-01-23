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

    public class TagsService : BaseDataService<Tag, int, IIntPKDeletableRepository<Tag>>, ITagsService
    {
        public TagsService(IIntPKDeletableRepository<Tag> tags, IIdentifierProvider idProvider)
            : base(tags, idProvider)
        {
        }

        public override Tag GetByEncodedId(string id)
        {
            var tag = this.Repository.GetById((int)this.IdentifierProvider.DecodeToIntId(id));
            return tag;
        }

        public override Tag GetByEncodedIdFromNotDeleted(string id)
        {
            var tag = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeToIntId(id));
            return tag;
        }
    }
}

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

    public class TagsService : BaseDataService<Tag, int, IIntPKDeletableRepository<Tag>>, ITagsService
    {
        public TagsService(IIntPKDeletableRepository<Tag> tags, IIdentifierProvider idProvider)
            : base(tags, idProvider)
        {
        }

        public override Tag GetByEncodedId(string id)
        {
            var tag = this.Repository.GetById(this.IdentifierProvider.DecodeIdToInt(id));
            return tag;
        }

        public override Tag GetByEncodedIdFromNotDeleted(string id)
        {
            var tag = this.Repository.GetByIdFromNotDeleted(this.IdentifierProvider.DecodeIdToInt(id));
            return tag;
        }
    }
}

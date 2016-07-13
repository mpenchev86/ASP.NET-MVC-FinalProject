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
        private readonly IIntPKDeletableRepository<Tag> tags;
        private IIdentifierProvider idProvider;

        public TagsService(IIntPKDeletableRepository<Tag> tags, IIdentifierProvider idProvider)
            : base(tags, idProvider)
        {
            this.tags = tags;
            this.idProvider = idProvider;
        }

        public override Tag GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var tag = this.tags.GetById(idAsInt);
            return tag;
        }

        public override Tag GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var tag = this.tags.GetByIdFromNotDeleted(idAsInt);
            return tag;
        }
    }
}

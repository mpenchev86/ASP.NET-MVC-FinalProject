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
        private readonly IIntPKDeletableRepository<Tag> tagsRepository;
        private IIdentifierProvider identifierProvider;

        public TagsService(IIntPKDeletableRepository<Tag> tags, IIdentifierProvider idProvider)
            : base(tags, idProvider)
        {
            this.tagsRepository = tags;
            this.identifierProvider = idProvider;
        }

        public override Tag GetByEncodedId(string id)
        {
            var tag = this.tagsRepository.GetById(this.identifierProvider.DecodeIdToInt(id));
            return tag;
        }

        public override Tag GetByEncodedIdFromNotDeleted(string id)
        {
            var tag = this.tagsRepository.GetByIdFromNotDeleted(this.identifierProvider.DecodeIdToInt(id));
            return tag;
        }
    }
}

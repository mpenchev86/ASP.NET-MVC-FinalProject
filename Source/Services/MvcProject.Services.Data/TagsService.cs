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

    public class TagsService : ITagsService
    {
        private readonly IRepository<Tag> tags;
        private IIdentifierProvider idProvider;

        public TagsService(IRepository<Tag> tags, IIdentifierProvider idProvider)
        {
            this.tags = tags;
            this.idProvider = idProvider;
        }

        public IQueryable<Tag> GetAll()
        {
            var result = this.tags
                             .All()
                             .OrderBy(x => x.Name);
            return result;
        }

        public Tag GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var tag = this.tags.GetById(idAsInt);
            return tag;
        }

        public Tag GetById(int id)
        {
            return this.tags.GetById(id);
        }
    }
}

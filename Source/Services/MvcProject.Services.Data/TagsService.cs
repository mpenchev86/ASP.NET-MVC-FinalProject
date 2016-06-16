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
            var result = this.tags.All().OrderBy(x => x.Name);
            return result;
        }

        public IQueryable<Tag> GetAllNotDeleted()
        {
            var result = this.tags.AllNotDeleted().OrderBy(x => x.Name);
            return result;
        }

        public Tag GetById(int id)
        {
            return this.tags.GetById(id);
        }

        public Tag GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var tag = this.tags.GetById(idAsInt);
            return tag;
        }

        public Tag GetByIdFromNotDeleted(int id)
        {
            return this.tags.GetByIdFromNotDeleted(id);
        }

        public Tag GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var tag = this.tags.GetByIdFromNotDeleted(idAsInt);
            return tag;
        }

        public void Insert(Tag entity)
        {
            this.tags.Add(entity);
            this.tags.SaveChanges();
        }

        public void Update(Tag entity)
        {
            this.tags.Update(entity);
            this.tags.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.tags.SaveChanges();
        }

        public void MarkAsDeleted(Tag entity)
        {
            entity.IsDeleted = true;
            this.tags.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.tags.SaveChanges();
        }

        public void DeletePermanent(Tag entity)
        {
            this.tags.DeletePermanent(entity);
            this.tags.SaveChanges();
        }
    }
}

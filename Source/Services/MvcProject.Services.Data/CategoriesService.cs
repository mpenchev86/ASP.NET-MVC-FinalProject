namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Services.Web;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categories;
        private IIdentifierProvider idProvider;

        public CategoriesService(IRepository<Category> categories, IIdentifierProvider idProvider)
        {
            this.categories = categories;
            this.idProvider = idProvider;
        }

        public IQueryable<Category> GetAll()
        {
            var result = this.categories.All().OrderBy(x => x.Name);
            return result;
        }

        public IQueryable<Category> GetAllNotDeleted()
        {
            var result = this.categories.AllNotDeleted().OrderBy(x => x.Name);
            return result;
        }

        public Category GetById(int id)
        {
            return this.categories.GetById(id);
        }

        public Category GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var category = this.categories.GetById(idAsInt);
            return category;
        }

        public Category GetByIdFromNotDeleted(int id)
        {
            return this.categories.GetByIdFromNotDeleted(id);
        }

        public Category GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var category = this.categories.GetByIdFromNotDeleted(idAsInt);
            return category;
        }

        public void Insert(Category entity)
        {
            this.categories.Add(entity);
            this.categories.SaveChanges();
        }

        public void Update(Category entity)
        {
            this.categories.Update(entity);
            this.categories.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.categories.SaveChanges();
        }

        public void MarkAsDeleted(Category entity)
        {
            entity.IsDeleted = true;
            this.categories.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.categories.SaveChanges();
        }

        public void DeletePermanent(Category entity)
        {
            this.categories.DeletePermanent(entity);
            this.categories.SaveChanges();
        }
    }
}

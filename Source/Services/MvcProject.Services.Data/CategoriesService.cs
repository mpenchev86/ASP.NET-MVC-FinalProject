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

        public Category GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var category = this.categories.GetById(idAsInt);
            return category;
        }

        public Category GetByIdFromAll(int id)
        {
            return this.categories.GetByIdFromAll(id);
        }

        public Category GetByIdFromAll(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var category = this.categories.GetByIdFromAll(idAsInt);
            return category;
        }

        public void Insert(Category propertyEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(Category propertyEntity)
        {
            throw new NotImplementedException();
        }

        public void MarkAsDeleted(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletePermanent(Category propertyEntity)
        {
            throw new NotImplementedException();
        }

        public void DeletePermanent(int id)
        {
            throw new NotImplementedException();
        }
    }
}

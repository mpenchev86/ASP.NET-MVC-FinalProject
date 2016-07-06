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

    public class ProductsService : IProductsService
    {
        private readonly IIntPKDeletableRepository<Product> products;
        private readonly IIntPKDeletableRepository<Tag> tags;
        private IIdentifierProvider idProvider;

        public ProductsService(
            IIntPKDeletableRepository<Product> products,
            IIntPKDeletableRepository<Tag> tags,
            IIdentifierProvider idProvider)
        {
            this.products = products;
            this.tags = tags;
            this.idProvider = idProvider;
        }

        public IQueryable<Product> GetAll()
        {
            var result = this.products.All().OrderBy(p => p.Id);
            return result;
        }

        public IQueryable<Product> GetAllNotDeleted()
        {
            var result = this.products.AllNotDeleted().OrderBy(p => p.Id);
            return result;
        }

        public Product GetById(int id)
        {
            return this.products.GetById(id);
        }

        public Product GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var product = this.products.GetById(idAsInt);
            return product;
        }

        public Product GetByIdFromNotDeleted(int id)
        {
            return this.products.GetByIdFromNotDeleted(id);
        }

        public Product GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var product = this.products.GetByIdFromNotDeleted(idAsInt);
            return product;
        }

        public void Insert(Product entity)
        {
            this.products.Add(entity);
            this.products.SaveChanges();
        }

        public void Update(Product entity)
        {
            this.products.Update(entity);
            this.products.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.products.SaveChanges();
        }

        public void MarkAsDeleted(Product entity)
        {
            entity.IsDeleted = true;
            this.products.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.products.SaveChanges();
        }

        public void DeletePermanent(Product entity)
        {
            this.products.DeletePermanent(entity);
            this.products.SaveChanges();
        }
    }
}

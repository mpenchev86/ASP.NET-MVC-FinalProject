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
        private readonly IRepository<Product> products;
        private IIdentifierProvider idProvider;

        public ProductsService(
            IRepository<Product> products,
            IIdentifierProvider idProvider)
        {
            this.products = products;
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

        public Product GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var product = this.products.GetById(idAsInt);
            return product;
        }

        public Product GetByIdFromAll(int id)
        {
            return this.products.GetByIdFromAll(id);
        }

        public Product GetByIdFromAll(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var product = this.products.GetByIdFromAll(idAsInt);
            return product;
        }

        public void Insert(Product propertyEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product propertyEntity)
        {
            throw new NotImplementedException();
        }

        public void MarkAsDeleted(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletePermanent(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletePermanent(Product propertyEntity)
        {
            throw new NotImplementedException();
        }
    }
}

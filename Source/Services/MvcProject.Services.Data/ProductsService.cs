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
        private IIdentifierProvider provider;

        public ProductsService(
            IRepository<Product> products,
            IIdentifierProvider provider)
        {
            this.products = products;
            this.provider = provider;
        }

        public IQueryable<Product> GetAllProducts()
        {
            var result = this.products
                             .All();
            return result;
        }

        public Product GetById(string id)
        {
            var idAsInt = this.provider.DecodeId(id);
            var product = this.products.GetById(idAsInt);
            return product;
        }

        public IQueryable<Product> GetRandomProducts(int count)
        {
            var result = this.products
                             .All()
                             .OrderBy(x => Guid.NewGuid())
                             .Take(count);
            return result;
        }
    }
}

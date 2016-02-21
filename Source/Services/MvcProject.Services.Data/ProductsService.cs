namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;

    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> products;

        public ProductsService(IRepository<Product> products)
        {
            this.products = products;
        }

        public IQueryable<Product> GetAllProducts()
        {
            var result = this.products
                             .All();
            return result;
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

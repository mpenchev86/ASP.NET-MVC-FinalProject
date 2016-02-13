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
        private readonly IRepository<SampleProduct> products;

        public ProductsService(IRepository<SampleProduct> products)
        {
            this.products = products;
        }

        public IQueryable<SampleProduct> GetAllProducts()
        {
            var result = this.products
                             .All();
            return result;
        }

        public IQueryable<SampleProduct> GetRandomProducts(int count)
        {
            var result = this.products
                             .All()
                             .OrderBy(x => Guid.NewGuid())
                             .Take(count);
            return result;
        }
    }
}

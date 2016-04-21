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
            var result = this.products.All();
            return result;
        }

        public Product GetById(string id)
        {
            //var product = this.products.GetById(id);
            var idAsInt = this.idProvider.DecodeId(id);
            var product = this.products.GetById(idAsInt);
            return product;
        }

        public void Create(Product model)
        {
            throw new NotImplementedException();
        }

        //public IQueryable<Product> GetRandomProducts(int count)
        //{
        //    var result = this.products
        //                     .All()
        //                     .OrderBy(x => Guid.NewGuid())
        //                     .Take(count);
        //    return result;
        //}
    }
}

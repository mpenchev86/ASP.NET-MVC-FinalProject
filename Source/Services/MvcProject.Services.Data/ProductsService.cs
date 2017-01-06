namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.Catalog;
    using MvcProject.Services.Web;

    public class ProductsService : BaseDataService<Product, int, IIntPKDeletableRepository<Product>>, IProductsService
    {
        public ProductsService(IIntPKDeletableRepository<Product> products, IIdentifierProvider idProvider)
            : base(products, idProvider)
        {
        }

        public override Product GetByEncodedId(string id)
        {
            var product = this.Repository.GetById((int)this.IdentifierProvider.DecodeIdToInt(id));
            return product;
        }

        public override Product GetByEncodedIdFromNotDeleted(string id)
        {
            var product = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeIdToInt(id));
            return product;
        }

        //public void UpdateWithConcurrencyCheck(Product entity)
        //{
        //}
    }
}

namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using JustOrderIt.Data.DbAccessConfig.Repositories;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Data.Models.Catalog;
    using JustOrderIt.Services.Web;

    public class ProductsService : BaseDataService<Product, int, IIntPKDeletableRepository<Product>>, IProductsService
    {
        public ProductsService(IIntPKDeletableRepository<Product> products, IIdentifierProvider idProvider)
            : base(products, idProvider)
        {
        }

        public override Product GetByEncodedId(string id)
        {
            var product = this.Repository.GetById((int)this.IdentifierProvider.DecodeToIntId(id));
            return product;
        }

        public override Product GetByEncodedIdFromNotDeleted(string id)
        {
            var product = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeToIntId(id));
            return product;
        }
    }
}

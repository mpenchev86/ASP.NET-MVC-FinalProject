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

    public class ProductsService : BaseDataService<Product, int, IIntPKDeletableRepository<Product>>, IProductsService
    {
        private readonly IIntPKDeletableRepository<Product> products;
        private IIdentifierProvider idProvider;

        public ProductsService(IIntPKDeletableRepository<Product> products, IIdentifierProvider idProvider)
            : base(products, idProvider)
        {
            this.products = products;
            this.idProvider = idProvider;
        }

        public override Product GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var product = this.products.GetById(idAsInt);
            return product;
        }

        public override Product GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var product = this.products.GetByIdFromNotDeleted(idAsInt);
            return product;
        }
    }
}

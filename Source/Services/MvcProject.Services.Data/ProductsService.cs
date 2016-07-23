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
        private readonly IIntPKDeletableRepository<Product> productsRepository;
        private IIdentifierProvider identifierProvider;

        public ProductsService(IIntPKDeletableRepository<Product> products, IIdentifierProvider idProvider)
            : base(products, idProvider)
        {
            this.productsRepository = products;
            this.identifierProvider = idProvider;
        }

        public override Product GetByEncodedId(string id)
        {
            var product = this.productsRepository.GetById(this.identifierProvider.DecodeIdToInt(id));
            return product;
        }

        public override Product GetByEncodedIdFromNotDeleted(string id)
        {
            var product = this.productsRepository.GetByIdFromNotDeleted(this.identifierProvider.DecodeIdToInt(id));
            return product;
        }
    }
}

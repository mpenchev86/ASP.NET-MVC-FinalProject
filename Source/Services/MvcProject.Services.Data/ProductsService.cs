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
        private IIdentifierProvider idProvider;

        public ProductsService(IIntPKDeletableRepository<Product> products, IIdentifierProvider idProvider)
            : base(products, idProvider)
        {
            this.productsRepository = products;
            this.idProvider = idProvider;
        }

        public override Product GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var product = this.productsRepository.GetById(idAsInt);
            return product;
        }

        public override Product GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var product = this.productsRepository.GetByIdFromNotDeleted(idAsInt);
            return product;
        }
    }
}

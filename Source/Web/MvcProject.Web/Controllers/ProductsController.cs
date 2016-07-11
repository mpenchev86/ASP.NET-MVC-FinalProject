namespace MvcProject.Web.Areas.Common.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using Services.Data;
    using Services.Web;
    using ViewModels.Home;

    public class ProductsController : BaseCommonController
    {
        private IProductsService productsService;
        private ICacheService cacheService;

        public ProductsController(
            IProductsService productsService,
            ICacheService cacheService)
        {
            this.productsService = productsService;
            this.cacheService = cacheService;
        }

        public ActionResult ById(string id)
        {
            var viewModel = this.cacheService.Get<ProductViewModel>(
                "Product_Id_" + id,
                () => this.Mapper.Map<ProductViewModel>(this.productsService.GetByEncodedId(id)),
                5 * 60);

            return this.View(viewModel);
        }
    }
}
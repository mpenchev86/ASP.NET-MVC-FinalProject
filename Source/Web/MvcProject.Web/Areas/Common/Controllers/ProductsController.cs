namespace MvcProject.Web.Areas.Common.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Services.Data;
    using ViewModels.Home;

    public class ProductsController : BaseController
    {
        private IProductsService productsService;

        public ProductsController(
            IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public ActionResult ById(string id)
        {
            var product = this.productsService.GetById(id);
            var viewModel = this.Mapper.Map<ProductViewModel>(product);
            return this.View(viewModel);
        }
    }
}
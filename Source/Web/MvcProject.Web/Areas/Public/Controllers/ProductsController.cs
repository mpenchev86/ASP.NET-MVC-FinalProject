namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using Services.Data;
    using Services.Web;
    using ViewModels.Products;

    public class ProductsController : BasePublicController
    {
        private IProductsService productsService;
        private IIdentifierProvider identifierProvider;

        public ProductsController(IProductsService productsService, IIdentifierProvider identifierProvider)
        {
            this.productsService = productsService;
            this.identifierProvider = identifierProvider;
        }

        // Get
        public PartialViewResult SneakPeak(string Id)
        {
            var product = this.productsService.GetById(this.identifierProvider.DecodeIdToInt(Id));
            var result = this.Mapper.Map<Product, ProductSneakPeakViewModel>(product);
            return this.PartialView("_SneakPeak", result);
        }
    }
}
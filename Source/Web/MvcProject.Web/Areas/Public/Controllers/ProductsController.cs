namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using Infrastructure.Extensions;
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

        // GET
        [HttpGet]
        public ActionResult Index(int Id)
        {
            var product = this.productsService.GetById(Id);
            var result = this.Mapper.Map<Product, ProductFullViewModel>(product);
            return this.View(result);
        }

        // GET
        [HttpGet]
        public PartialViewResult SneakPeak(string Id)
        {
            var product = this.productsService.GetById(this.identifierProvider.DecodeIdToInt(Id));
            var result = this.Mapper.Map<Product, ProductSneakPeakViewModel>(product);
            return this.PartialView("_SneakPeak", result);
        }

        // GET
        public ActionResult AllProductsOfCategory(string category)
        {
            var products = this.productsService.GetAll().Where(p => p.Category.Name == category);
            var result = products.To<ProductOfCategoryViewModel>();
            return this.View(result);
        }

        // GET
        public ActionResult AllProductsWithTag(string tag)
        {
            var products = this.productsService.GetAll().Where(p => p.Tags.Select(t => t.Name).Contains(tag));
            var result = products.To<ProductWithTagViewModel>();
            return this.View(result);
        }
    }
}
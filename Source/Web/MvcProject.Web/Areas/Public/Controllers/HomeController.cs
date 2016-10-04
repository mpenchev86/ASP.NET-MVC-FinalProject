namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MvcProject.Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Products;
    using Services.Web;
    using Data.Models;

    public class HomeController : BasePublicController
    {
        private IProductsService productsService;

        public HomeController(IProductsService productsService, ICacheService cache)
        {
            this.productsService = productsService;
            this.Cache = cache;
        }

        // [OutputCache(Duration = 30 * 60, Location = OutputCacheLocation.Server, VaryByCustom = "SomeOtherIdentifier")]
        //[OutputCache(Duration = 10 * 60)]
        public ActionResult Index()
        {
            return this.View();
        }

        //[OutputCache(Duration = 10 * 60)]
        public PartialViewResult Carousel()
        {
            //var viewModel = new List<CarouselData>();

            var viewModel = this.Cache.Get(
                "carouselProducts",
                () => this.productsService
                    .GetAll()
                    .OrderByDescending(p => p.CreatedOn)
                    .Take(5)
                    .To<CarouselData>()
                    .ToList(),
                2 * 60);

            return PartialView("_Carousel", viewModel);
        }

        // Cached
        //[OutputCache(Duration = 10 * 60)]
        public JsonResult ReadNewestProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "newestProducts",
                () => this.productsService
                    .GetAll()
                    .OrderByDescending(p => p.CreatedOn)
                    .Take(UiSpecificConstants.IndexListViewNumberOfNewestProducts)
                    .To<ProductDetailsForIndexListView>()
                    .ToList(),
                CacheConstants.IndexListViewCacheDurationInSeconds);

            return this.Json(viewModel.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        // Cached
        //[OutputCache(Duration = 10 * 60)]
        public JsonResult ReadBestSellingProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "bestSellingProducts",
                () => this.productsService
                    .GetAll()
                    .OrderByDescending(p => p.AllTimeItemsSold)
                    .Take(UiSpecificConstants.IndexListViewNumberOfBestSellingProducts)
                    .To<ProductDetailsForIndexListView>()
                    .ToList(),
                CacheConstants.IndexListViewCacheDurationInSeconds);

            return this.Json(viewModel.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        // Cached
        //[OutputCache(Duration = 10 * 60)]
        public JsonResult ReadHighestVotedProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "highestVotedProducts",
                () => this.productsService
                    .GetAll()
                    .OrderByDescending(Product.GetAverageRating)
                    .Take(UiSpecificConstants.IndexListViewNumberOfhighestVotedProducts)
                    .To<ProductDetailsForIndexListView>()
                    .ToList(),
                CacheConstants.IndexListViewCacheDurationInSeconds);

            return this.Json(viewModel.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            return this.PartialView("UnderConstruction", null);
        }

        public ActionResult DeliveryInfo()
        {
            return this.PartialView("UnderConstruction", null);
        }

        public ActionResult ReturnPolicy()
        {
            return this.PartialView("UnderConstruction", null);
        }

        public ActionResult JobOpennings()
        {
            return this.PartialView("UnderConstruction", null);
        }

        public ActionResult Contacts()
        {
            return this.PartialView("UnderConstruction", null);
        }

        public ActionResult Help()
        {
            return this.PartialView("UnderConstruction", null);
        }
    }
}
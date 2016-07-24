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
    using ViewModels.Home;
    using Services.Web;

    public class HomeController : BasePublicController
    {
        private IProductsService productsService;

        public HomeController(IProductsService productsService, ICacheService cache)
        {
            this.productsService = productsService;
            this.Cache = cache;
        }

        // [OutputCache(Duration = 30 * 60, Location = OutputCacheLocation.Server, VaryByCustom = "SomeOtherIdentifier")]
        public ActionResult Index()
        {
            return this.View();
        }

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
        public JsonResult ReadNewestProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "newestProducts",
                () => this.productsService
                    .GetAll()
                    .OrderByDescending(p => p.CreatedOn)
                    .Take(AppSpecificConstants.IndexListViewNumberOfNewestProducts)
                    .To<ProductDetailsForIndexListView>()
                    .ToList(),
                AppSpecificConstants.IndexListViewCacheDurationInSeconds);

            return this.Json(viewModel.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        // Cached
        public JsonResult ReadBestSellingProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "bestSellingProducts",
                () => this.productsService
                    .GetAll()
                    .OrderByDescending(p => p.AllTimeItemsSold)
                    .Take(AppSpecificConstants.IndexListViewNumberOfBestSellingProducts)
                    .To<ProductDetailsForIndexListView>()
                    .ToList(),
                AppSpecificConstants.IndexListViewCacheDurationInSeconds);

            return this.Json(viewModel.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        // Cached
        public JsonResult ReadHighestVotedProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "highestVotedProducts",
                () => this.productsService
                    .GetAll()
                    .To<ProductDetailsForIndexListView>()
                    .OrderByDescending(p => p.AllTimeAverageRating)
                    .Take(AppSpecificConstants.IndexListViewNumberOfhighestVotedProducts)
                    .ToList(),
                AppSpecificConstants.IndexListViewCacheDurationInSeconds);

            return this.Json(viewModel.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
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
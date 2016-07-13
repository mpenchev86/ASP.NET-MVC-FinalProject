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

    public class HomeController : BasePublicController
    {
        private IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        // [OutputCache(Duration = 30 * 60, Location = OutputCacheLocation.Server, VaryByCustom = "SomeOtherIdentifier")]
        public ActionResult Index()
        {
            return this.View();
        }

        // Cached
        public ActionResult ReadNewestProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "newestProducts",
                () => this.productsService
                    .GetAll()
                    .To<ProductDetailsForIndexListView>()
                    .OrderByDescending(p => p.CreatedOn)
                    .Take(ApplicationSpecificConstants.IndexListViewNumberOfNewestProducts)
                    .ToList(),
                ApplicationSpecificConstants.IndexListViewCacheDurationInSeconds)
                ;

            return this.Json(viewModel.ToDataSourceResult(request));
        }

        // Cached
        public ActionResult ReadBestSellingProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "bestSellingProducts",
                () => this.productsService
                    .GetAll()
                    .To<ProductDetailsForIndexListView>()
                    .OrderByDescending(p => p.AllTimeItemsSold)
                    .Take(ApplicationSpecificConstants.IndexListViewNumberOfBestSellingProducts)
                    .ToList()
                    ,
                ApplicationSpecificConstants.IndexListViewCacheDurationInSeconds)
                ;

            return this.Json(viewModel.ToDataSourceResult(request));
        }

        // Cached
        public ActionResult ReadHighestVotedProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "highestVotedProducts",
                () => this.productsService
                    .GetAll()
                    .To<ProductDetailsForIndexListView>()
                    .OrderByDescending(p => p.AllTimeAverageRating)
                    .Take(ApplicationSpecificConstants.IndexListViewNumberOfhighestVotedProducts)
                    .ToList(),
                ApplicationSpecificConstants.IndexListViewCacheDurationInSeconds)
                ;

            return this.Json(viewModel.ToDataSourceResult(request));
        }
    }
}
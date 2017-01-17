namespace JustOrderIt.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using JustOrderIt.Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Products;
    using Services.Web.CacheServices;
    using Data.Models;
    using ViewModels.Categories;
    using System.Web.Caching;
    using ViewModels.Search;

    public class HomeController : BasePublicController
    {
        private readonly ICacheService cacheService;
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;

        public HomeController(
            IProductsService productsService, 
            ICacheService cacheService,
            ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.cacheService = cacheService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [ChildActionOnly]
        public PartialViewResult Carousel()
        {
            var viewModel = this.cacheService.Get(
                "layoutCarouselData",
                () => productsService
                    .GetAll()
                    .OrderByDescending(p => p.CreatedOn)
                    .Take(5)
                    .To<CarouselData>()
                    .ToList(),
                CacheConstants.CarouselDataCacheExpiration);

            return PartialView(viewModel);
        }

        public PartialViewResult NavLowerLeft()
        {
            var categories = this.GetCategories();
            return this.PartialView(categories);
        }

        public PartialViewResult NavLowerMiddle()
        {
            this.ViewData["categories"] = this.GetCategories().Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
            return this.PartialView(new SearchViewModel());
        }

        [OutputCache(Duration = 15 * 60)]
        public JsonResult ReadNewestProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.cacheService.Get(
                "newestProducts",
                () => this.productsService
                    .GetAll()
                    .OrderByDescending(p => p.CreatedOn)
                    .Take(UiSpecificConstants.IndexListViewNumberOfNewestProducts)
                    .To<ProductDetailsForIndexListView>()
                    .ToList(),
                CacheConstants.IndexListViewCacheExpiration);

            return this.Json(viewModel.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 15 * 60)]
        public JsonResult ReadBestSellingProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.cacheService.Get(
                "bestSellingProducts",
                () => this.productsService
                    .GetAll()
                    .OrderByDescending(p => p.AllTimeItemsSold)
                    .Take(UiSpecificConstants.IndexListViewNumberOfBestSellingProducts)
                    .To<ProductDetailsForIndexListView>()
                    .ToList(),
                CacheConstants.IndexListViewCacheExpiration);

            return this.Json(viewModel.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 15 * 60)]
        public JsonResult ReadHighestVotedProducts([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.cacheService.Get(
                "highestVotedProducts",
                () => this.productsService
                    .GetAll()
                    .OrderByDescending(p => p.Votes.Average(v => v.VoteValue))
                    .Take(UiSpecificConstants.IndexListViewNumberOfhighestVotedProducts)
                    .To<ProductDetailsForIndexListView>()
                    .ToList(),
                CacheConstants.IndexListViewCacheExpiration);

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

        #region Helpers
        private List<CategoryForLayoutDropDown> GetCategories()
        {
            var categories = this.cacheService.Get(
                "categoriesForQuerySearch",
                () => this.categoriesService
                    .GetAll()
                    .To<CategoryForLayoutDropDown>()
                    .OrderBy(i => i.Name)
                    .ToList(),
                30 * 60);

            return categories;
        }
        #endregion
    }
}
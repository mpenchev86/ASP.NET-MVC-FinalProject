namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Areas.Public.Controllers;
    using Areas.Public.ViewModels.Products;
    using Areas.Public.ViewModels.Search;
    using Common.GlobalConstants;
    using Data.Models.Catalog;
    using Data.Models.Search;
    using Hangfire;
    using Hangfire.Common;
    using Hangfire.SqlServer;
    using Hangfire.States;
    using Infrastructure.BackgroundWorkers;
    using Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data;
    using Services.Web.CacheServices;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class SearchControllerTests
    {
        private AutoMapperConfig autoMapperConfig;
        private Mock<IAutoUpdateCacheService> autoUpdateCacheServiceMock;
        private Mock<IProductsService> productsServiceMock;
        private Mock<ISearchFiltersService> searchFiltersServiceMock;
        private Mock<ICategoriesService> categoriesServiceMock;
        private Mock<IKeywordsService> keywordsServiceMock;

        public SearchControllerTests()
        {
            this.PrepareController();
        }

        [Test]
        public void SearchBarShouldRedirectCorrectlyIfCategoryAPositiveInteger()
        {
            // Arrange
            var viewModel = new SearchViewModel() { CategoryId = 3, };
            //var routeValues = new RouteValueDictionary
            //{
            //    { "categoryId", viewModel.CategoryId },
            //    { "query", viewModel.Query },
            //    { "area", Areas.PublicAreaName },
            //    { "action", "SearchByCategory" },
            //    { "controller", "Search" }
            //};

            //var routeDataMock = new Mock<RouteData>();
            //routeDataMock.SetupGet(x => x.Values).Returns(routeValues);
            //var controllerContextMock = new Mock<ControllerContext>();
            //controllerContextMock.SetupSet(x => x.RouteData = routeDataMock.Object);

            // Act
            var controller = new SearchController(this.autoUpdateCacheServiceMock.Object, this.productsServiceMock.Object, this.searchFiltersServiceMock.Object, this.categoriesServiceMock.Object, this.keywordsServiceMock.Object);
            //controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.SearchBar(/*It.IsAny<SearchViewModel>()*/viewModel))
                .ShouldRedirectTo(x => x.SearchByCategory(It.IsAny<int>(), It.IsAny<string>(), 0));
            //var result = controller.SearchBar(viewModel) as RedirectToRouteResult;
            //CollectionAssert.AreEquivalent(routeValues, result.RouteValues);

            //redirectResult
            //   .RouteValues
            //   .ShouldBeEquivalentTo(expectedRedirectValues,
            //   "The redirect should look as I expect, including the parameters");
        }

        [Test]
        public void SearchBarShouldRedirectCorrectlyIfQueryStringIsNotNullOrWhiteSpace()
        {
            // Arrange
            var viewModel = new SearchViewModel() { Query = "jkdhsjbe", };

            // Act
            var controller = new SearchController(this.autoUpdateCacheServiceMock.Object, this.productsServiceMock.Object, this.searchFiltersServiceMock.Object, this.categoriesServiceMock.Object, this.keywordsServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.SearchBar(viewModel))
                .ShouldRedirectTo(x => x.SearchByQuery(It.IsAny<string>()));
        }

        [Test]
        public void SearchBarShouldRedirectCorrectlyIfCategoryIdIsNotPositiveAndQueryStringIsNullOrWhiteSpace()
        {
            // Act
            var controller = new SearchController(this.autoUpdateCacheServiceMock.Object, this.productsServiceMock.Object, this.searchFiltersServiceMock.Object, this.categoriesServiceMock.Object, this.keywordsServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.SearchBar(new SearchViewModel()))
                .ShouldRedirectTo<HomeController>(x => x.Index());
        }

        [Test]
        public void RefineCategorySearchShouldRedirectCorrectly()
        {
            // Arrange
            int categoryId = 34;
            string query = "jkfha hfjsjnkdfjksdhfsdbfnsdbvgmbj";

            List<SearchFilterForCategoryViewModel> searchFiltersMock = new List<SearchFilterForCategoryViewModel>()
            {
                new SearchFilterForCategoryViewModel
                {
                    RefinementOptions = new List<SearchFilterOptionViewModel>()
                    {
                        new SearchFilterOptionViewModel { Checked = false, },
                        new SearchFilterOptionViewModel { Checked = true, },
                        new SearchFilterOptionViewModel { Checked = false, },
                    },
                },
                new SearchFilterForCategoryViewModel
                {
                    RefinementOptions = new List<SearchFilterOptionViewModel>()
                    {
                        new SearchFilterOptionViewModel { Checked = true, },
                        new SearchFilterOptionViewModel { Checked = false, },
                    },
                },
            };

            // Act
            var controller = new SearchController(this.autoUpdateCacheServiceMock.Object, this.productsServiceMock.Object, this.searchFiltersServiceMock.Object, this.categoriesServiceMock.Object, this.keywordsServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.RefineCategorySearch(categoryId, query, searchFiltersMock))
                .ShouldRedirectTo(x => x.SearchByCategory(categoryId, query, It.IsAny<long>()));
        }

        [Test]
        public void AllProductsWithTagShouldReturnCorrectly()
        {
            // Arrange
            var dbProductsMock = new List<Product>() { };
            this.productsServiceMock.Setup(x => x.GetAll()).Returns(dbProductsMock.AsQueryable);

            // Act
            var controller = new SearchController(this.autoUpdateCacheServiceMock.Object, this.productsServiceMock.Object, this.searchFiltersServiceMock.Object, this.categoriesServiceMock.Object, this.keywordsServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.AllProductsWithTag(It.IsAny<string>()))
                .ShouldRenderDefaultView()
                .WithModel<List<ProductWithTagViewModel>>();
        }

        [Ignore("Cache service mock .Get<> fails to retrieve keywords. Returns null.")]
        public void SearchAutoCompleteShouldReturnCorrectly()
        {
            // Arrange
            var cacheKey = "someCacheKeyString";
            var dbKeywordsMock = new List<Keyword>
            {
                new Keyword { SearchTerm = "somekeyword" },
                new Keyword { SearchTerm = "another" },
                new Keyword { SearchTerm = "jchbui82786bhub" },
            };
            var keywordStrings = dbKeywordsMock.Select(k => k.SearchTerm).ToList();
            var options = new SqlServerStorageOptions()
            {
                
            };
            JobStorage.Current = new SqlServerStorage("HangfireConnection", options);
            var backgroundClientMock = new Mock<IBackgroundJobClient>();
            backgroundClientMock.Setup(c => c.Create(It.IsAny<Job>(), It.IsAny<EnqueuedState>()));
            var backgroundServiceMock = new Mock<IBackgroundJobsService>();
            backgroundServiceMock.SetupGet(x => x.JobClient).Returns(backgroundClientMock.Object);
            this.autoUpdateCacheServiceMock.SetupGet(x => x.BackgroundService).Returns(backgroundServiceMock.Object);
            this.autoUpdateCacheServiceMock.SetupSet(x => x.BackgroundService = backgroundServiceMock.Object);
            this.autoUpdateCacheServiceMock.Setup(x => x.UpdateAuxiliaryCacheValue(cacheKey, keywordStrings));
            this.autoUpdateCacheServiceMock.Setup(x => x.Get<List<string>, SearchController>(
                cacheKey,
                /*It.IsAny<Func<List<string>>>()*/() => keywordStrings,
                /*It.IsAny<int>()*/3 * 60,
                /*It.IsAny<string>()*/"GetAndCacheKeywords",
                new object[] { cacheKey },
                /*It.IsAny<int>()*/2 * 60,
                CacheItemPriority.Default)
                )
                .Returns(keywordStrings);
            this.keywordsServiceMock.Setup(x => x.GetAll()).Returns(dbKeywordsMock.AsQueryable);

            // Act
            var controller = new SearchController(this.autoUpdateCacheServiceMock.Object, this.productsServiceMock.Object, this.searchFiltersServiceMock.Object, this.categoriesServiceMock.Object, this.keywordsServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.SearchAutoComplete("jgbwi"))
                .ShouldReturnJson(
                data =>
                {
                    Assert.IsNotNull(data);
                    Assert.IsInstanceOf(typeof(List<string>), data);
                    CollectionAssert.AllItemsAreNotNull(data as List<string>);
                }
                );

            Assert.AreEqual(controller.SearchAutoComplete(It.IsAny<string>()).JsonRequestBehavior, JsonRequestBehavior.AllowGet);
        }

        #region Helpers
        private void PrepareController()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(SearchController).Assembly, typeof(IBackgroundJobsService).Assembly, typeof(IAutoUpdateCacheService).Assembly);
            this.autoUpdateCacheServiceMock = new Mock<IAutoUpdateCacheService>();
            this.productsServiceMock = new Mock<IProductsService>();
            this.searchFiltersServiceMock = new Mock<ISearchFiltersService>();
            this.categoriesServiceMock = new Mock<ICategoriesService>();
            this.keywordsServiceMock = new Mock<IKeywordsService>();
        }
        #endregion
    }
}

namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Areas.Public.Controllers;
    using Areas.Public.ViewModels.Search;
    using Common.GlobalConstants;
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
        public void SearchAutoCompleteShouldReturnCorrectly()
        {
            // Arrange
            // Act
            // Assert
        }

        private void PrepareController()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(SearchController).Assembly);
            this.autoUpdateCacheServiceMock = new Mock<IAutoUpdateCacheService>();
            this.productsServiceMock = new Mock<IProductsService>();
            this.searchFiltersServiceMock = new Mock<ISearchFiltersService>();
            this.categoriesServiceMock = new Mock<ICategoriesService>();
            this.keywordsServiceMock = new Mock<IKeywordsService>();
        }
    }
}

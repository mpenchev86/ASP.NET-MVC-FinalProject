namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Areas.Public.Controllers;
    using Areas.Public.ViewModels.Search;
    using Common.GlobalConstants;
    using Data.Models.Catalog;
    using Data.Models.Search;
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
            //var dbCategoriesMock = new List<Category>()
            //{
            //    new Category
            //    {
            //        Id = 2,
            //        Name = "jkasdb",
            //        SearchFilters = new List<SearchFilter>(),
            //    },
            //    new Category
            //    {
            //        Id = 234,
            //        Name = "knhnhkwlje",
            //        SearchFilters = new List<SearchFilter>(),
            //    },
            //    new Category
            //    {
            //        Id = 73,
            //        Name = "someCat",
            //        SearchFilters = new List<SearchFilter>(),
            //    },
            //    new Category
            //    {
            //        Id = 86,
            //        Name = "jjjjj",
            //        SearchFilters = new List<SearchFilter>(),
            //    },
            //};

            //this.categoriesServiceMock.Setup(x => x.GetAll()).Returns(dbCategoriesMock.AsQueryable);
            int categoryId = 34;
            string query = "jkfha hfjsjnkdfjksdhfsdbfnsdbvgmbj";

            List<SearchFilterForCategoryViewModel> searchFilters = new List<SearchFilterForCategoryViewModel>()
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
            controller.WithCallTo(x => x.RefineCategorySearch(/*It.IsAny<int>()*/categoryId, /*It.IsAny<string>()*/query, /*It.IsAny<List<SearchFilterForCategoryViewModel>>()*/searchFilters))
                .ShouldRedirectTo(x => x.SearchByCategory(/*It.IsAny<int>()*/categoryId, /*It.IsAny<string>()*/query, /*0*/It.IsAny<long>()));
        }

        //[Test]
        //public void SearchAutoCompleteShouldReturnCorrectly()
        //{
        //    // Arrange
        //    var cacheKey = /*"someCacheKeyString"*/CacheConstants.AllCategoriesKeywords;
        //    var dbKeywordsMock = new List<Keyword>
        //    {
        //        new Keyword { SearchTerm = "somekeyword" },
        //        new Keyword { SearchTerm = "another" },
        //        new Keyword { SearchTerm = "jchbui82786bhub" },
        //    };
        //    var keywordStrings = dbKeywordsMock.Select(k => k.SearchTerm).ToList();
        //    var backgroundClientMock = new Mock<IBackgroundJobClient>();
        //    //backgroundClientMock.
        //    //GlobalConfiguration.Configuration.UseSqlServerStorage(DbAccess.HangfireConnectionStringName);
        //    var backgroundServiceMock = new Mock<IBackgroundJobsService>();
        //    //backgroundServiceMock.SetupGet(x => x.JobClient).Returns(backgroundClientMock.Object);
        //    this.autoUpdateCacheServiceMock.SetupGet(x => x.BackgroundService).Returns(backgroundServiceMock.Object);
        //    this.autoUpdateCacheServiceMock.SetupSet(x => x.BackgroundService = backgroundServiceMock.Object);
        //    this.autoUpdateCacheServiceMock.Setup(x => x.Get<List<string>, SearchController>(
        //        /*It.IsAny<string>()*/ cacheKey,
        //        /*It.IsAny<Func<List<string>>>()*/ () => keywordStrings,
        //        It.IsAny<int>()/*3 * 60*/,
        //        /*It.IsAny<string>()*/"GetAndCacheKeywords", 
        //        new object[] { cacheKey },
        //        It.IsAny<int>()/*2 * 60*/, 
        //        CacheItemPriority.Default)
        //        )
        //        .Returns(keywordStrings/*new List<string>()*/);
        //    this.autoUpdateCacheServiceMock.Setup(x => x.UpdateAuxiliaryCacheValue(cacheKey, keywordStrings));
        //    this.keywordsServiceMock.Setup(x => x.GetAll()).Returns(dbKeywordsMock.AsQueryable);

        //    // Act
        //    var controller = new SearchController(this.autoUpdateCacheServiceMock.Object, this.productsServiceMock.Object, this.searchFiltersServiceMock.Object, this.categoriesServiceMock.Object, this.keywordsServiceMock.Object);

        //    // Assert
        //    controller.WithCallTo(x => x.SearchAutoComplete(/*It.IsAny<string>()*/"som"))
        //        .ShouldReturnJson(
        //        //data => 
        //        //{
        //        //    Assert.IsNotNull(data);
        //        //    //Assert.IsInstanceOf(typeof(List<string>), data);
        //        //    //CollectionAssert.AllItemsAreNotNull(data as List<string>);
        //        //}
        //        );

        //    //Assert.AreEqual(controller.SearchAutoComplete(It.IsAny<string>()).JsonRequestBehavior, JsonRequestBehavior.AllowGet);
        //}

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
    }
}

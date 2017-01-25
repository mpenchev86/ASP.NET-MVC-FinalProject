namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using NUnit.Framework;
    using Infrastructure.Mapping;
    using Areas.Public.Controllers;
    using Moq;
    using Services.Data;
    using Services.Web.CacheServices;
    using TestStack.FluentMVCTesting;
    using Data.Models.Catalog;
    using Areas.Public.ViewModels.Products;
    using Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Areas.Public.ViewModels.Categories;
    using Areas.Public.ViewModels.Search;
    using Kendo.Mvc.UI;

    [TestFixture]
    public class HomeControllerTests
    {
        private AutoMapperConfig autoMapperConfig;
        private Mock<IProductsService> productsServiceMock;
        private Mock<ICacheService> cacheServiceMock;
        private Mock<ICategoriesService> categoriesServiceMock;

        [Test]
        public void IndexShouldWorkCorrectly()
        {
            // Arrange
            this.PrepareController();

            // Atc
            var controller = new HomeController(productsServiceMock.Object, cacheServiceMock.Object, categoriesServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CarouselShouldWorkCorrectly()
        {
            // Arrange
            this.PrepareController();
            var mockDbData = this.GetMockDbProducts();
            this.productsServiceMock.Setup(x => x.GetAll()).Returns(mockDbData as IQueryable<Product>);

            var cachedData = mockDbData.AsQueryable().To<CarouselData>().ToList();
            this.cacheServiceMock.Setup(x => x
                .Get(It.IsAny<string>(), It.IsAny<Func<List<CarouselData>>>(), It.IsAny<int>()))
                .Returns(cachedData);
            
            // Atc
            var controller = new HomeController(this.productsServiceMock.Object, this.cacheServiceMock.Object, this.categoriesServiceMock.Object);

            // Assert
            controller.WithCallToChild(x => x.Carousel())
                .ShouldRenderDefaultPartialView()
                .WithModel<List<CarouselData>>(
                    vm =>
                    {
                        CollectionAssert.AllItemsAreUnique(vm);
                        CollectionAssert.AreEquivalent(cachedData, vm);
                        Assert.AreEqual(cachedData, vm);
                    })
                .AndNoModelErrors();

            var result = controller.Carousel() as PartialViewResult;
            var model = result.Model as List<CarouselData>;
            Assert.IsNotNull(result);
        }

        [Test]
        public void NavLowerLeftShouldWorkCorrectly()
        {
            // Arrange
            this.PrepareController();
            var mockDbData = this.GetMockDbCategories();
            this.categoriesServiceMock.Setup(x => x.GetAll()).Returns(mockDbData as IQueryable<Category>);

            var cachedData = mockDbData.AsQueryable().To<CategoryForLayoutDropDown>().ToList();
            this.cacheServiceMock.Setup(x => x
                .Get(It.IsAny<string>(), It.IsAny<Func<List<CategoryForLayoutDropDown>>>(), It.IsAny<int>()))
                .Returns(cachedData);

            // Atc
            var controller = new HomeController(productsServiceMock.Object, cacheServiceMock.Object, categoriesServiceMock.Object);

            // Assert
            controller.WithCallToChild(x => x.NavLowerLeft())
                .ShouldRenderDefaultPartialView()
                .WithModel<List<CategoryForLayoutDropDown>>(vm =>
                {
                    CollectionAssert.AreEquivalent(cachedData, vm);
                })
                .AndNoModelErrors();

            var result = controller.NavLowerLeft() as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            CollectionAssert.AllItemsAreInstancesOfType(result.Model as List<CategoryForLayoutDropDown>, typeof(CategoryForLayoutDropDown));
        }

        [Test]
        public void NavLowerMiddleShouldWorkCorrectly()
        {
            // Arrange
            this.PrepareController();
            var mockDbData = GetMockDbCategories();
            this.categoriesServiceMock.Setup(x => x.GetAll()).Returns(mockDbData as IQueryable<Category>);

            var cachedData = mockDbData.AsQueryable().To<CategoryForLayoutDropDown>();
            this.cacheServiceMock.Setup(x => x
                .Get(It.IsAny<string>(), It.IsAny<Func<List<CategoryForLayoutDropDown>>>(), It.IsAny<int>()))
                .Returns(cachedData.ToList());

            // Atc
            var controller = new HomeController(productsServiceMock.Object, cacheServiceMock.Object, categoriesServiceMock.Object);

            // Assert
            controller.WithCallToChild(x => x.NavLowerMiddle())
                .ShouldRenderDefaultPartialView()
                .WithModel<SearchViewModel>(vm => 
                {
                    Assert.IsInstanceOf<SearchViewModel>(vm);
                })
                .AndNoModelErrors();

            var result = controller.NavLowerMiddle() as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOf<SearchViewModel>(result.Model);
        }

        [Test]
        public void ReadNewestProductsReturnsCorrectData()
        {
            // Arrange
            this.PrepareController();
            var request = new DataSourceRequest();
            var mockDbData = this.GetMockDbProducts();
            this.productsServiceMock.Setup(x => x.GetAll()).Returns(mockDbData as IQueryable<Product>);

            var cachedData = mockDbData.AsQueryable().To<ProductDetailsForIndexListView>().ToList();
            this.cacheServiceMock.Setup(x => x
                .Get(It.IsAny<string>(), It.IsAny<Func<List<ProductDetailsForIndexListView>>>(), It.IsAny<int>()))
                .Returns(cachedData);

            // Act
            var controller = new HomeController(productsServiceMock.Object, cacheServiceMock.Object, categoriesServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.ReadNewestProducts(request))
                .ShouldReturnJson(
                data =>
                {
                    Assert.IsNotNull(data);
                    Assert.IsInstanceOf<DataSourceResult>(data);
                    CollectionAssert.AllItemsAreInstancesOfType((data as DataSourceResult).Data, typeof(ProductDetailsForIndexListView));
                }
                );

            var result = controller.ReadNewestProducts(request);
            Assert.AreEqual(result.JsonRequestBehavior, JsonRequestBehavior.AllowGet);
        }

        #region Helpers
        private void PrepareController()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(HomeController).Assembly);
            this.productsServiceMock = new Mock<IProductsService>();
            this.cacheServiceMock = new Mock<ICacheService>();
            this.categoriesServiceMock = new Mock<ICategoriesService>();
        }

        private List<Product> GetMockDbProducts()
        {
            return new List<Product>()
            {
                new Product() { Id = 1, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
                new Product() { Id = 2, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
                new Product() { Id = 3, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
                new Product() { Id = 4, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
                new Product() { Id = 5, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
            };
        }

        private List<Category> GetMockDbCategories()
        {
            return new List<Category>()
            {
                new Category() { Id = 1, Name = "bcjhgwuheufhm" },
                new Category() { Id = 2, Name = "bcjhgwuheufhm" },
                new Category() { Id = 3, Name = "bcjhgwuheufhm" },
            };
        }
        #endregion
    }
}

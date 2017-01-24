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
    using Moq.Language.Flow;
    using Services.Data;
    using Services.Web.CacheServices;
    using TestStack.FluentMVCTesting;
    using Data.Models.Catalog;
    using Areas.Public.ViewModels.Products;
    using Infrastructure.Extensions;
    using Common.GlobalConstants;

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
                .ShouldRenderView("Index");
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CarouselShouldWorkCorrectly()
        {
            // Arrange
            this.PrepareController();
            var dbProductsMock = new List<Product>()
            {
                new Product() { Id = 1, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
                new Product() { Id = 2, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
                new Product() { Id = 3, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
                new Product() { Id = 4, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
                new Product() { Id = 5, Title = "jhsgdf", CategoryId = 2, QuantityInStock = 312, UnitPrice = 64.8m, SellerId = "jvui89u893fnh89hn49uj", CreatedOn = DateTime.Now },
            };

            var carouselData = dbProductsMock.AsQueryable().To<CarouselData>().ToList();
            //var carouselData = new List<CarouselData>()
            //{
            //    new CarouselData() { Id = 1, Title = "jhsgdf" },
            //    new CarouselData() { Id = 2, Title = "jhsgdf" },
            //    new CarouselData() { Id = 3, Title = "jhsgdf" },
            //    new CarouselData() { Id = 4, Title = "jhsgdf" },
            //    new CarouselData() { Id = 5, Title = "jhsgdf" },
            //};

            this.productsServiceMock.Setup(x => x.GetAll()).Returns(/*It.IsAny<IQueryable<Product>>()*/ dbProductsMock as IQueryable<Product>);

            this.cacheServiceMock.Setup(x => x
                .Get/*<List<CarouselData>>*/(/*"layoutCarouselData"*/It.IsAny<string>(),
                /*this.GetCarouselData()*/It.IsAny<Func<List<CarouselData>>>(),
                /*CacheConstants.CarouselDataCacheExpiration)*/It.IsAny<int>()))
                .Returns(/*dbProductsMock.AsQueryable().To<CarouselData>().ToList()*/
                    carouselData
                    );
            
            // Atc
            var controller = new HomeController(this.productsServiceMock.Object, this.cacheServiceMock.Object, this.categoriesServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.Carousel())
                .ShouldRenderPartialView("Carousel")
                .WithModel<List<CarouselData>>(
                    vm =>
                    {
                        Assert.AreEqual(carouselData, vm);
                    })
                .AndNoModelErrors();

            var result = controller.Carousel() as PartialViewResult;
            var model = result.Model as List<CarouselData>;
            Assert.IsNotNull(result);
        }

        #region Helpers
        private Func<List<CarouselData>> GetCarouselData()
        {
            return () => this.productsServiceMock.Object
                    .GetAll()
                    .OrderByDescending(p => p.CreatedOn)
                    .Take(5)
                    .To<CarouselData>()
                    .ToList();
        }

        private void PrepareController()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(HomeController).Assembly);
            this.productsServiceMock = new Mock<IProductsService>();
            this.cacheServiceMock = new Mock<ICacheService>();
            this.categoriesServiceMock = new Mock<ICategoriesService>();
        }
        #endregion
    }
}

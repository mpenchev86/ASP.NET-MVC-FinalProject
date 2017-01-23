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

        private void PrepareController()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(HomeController).Assembly);
            this.productsServiceMock = new Mock<IProductsService>();
            this.cacheServiceMock = new Mock<ICacheService>();
            this.categoriesServiceMock = new Mock<ICategoriesService>();
        }
    }
}

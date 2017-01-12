namespace MvcProject.Web.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Areas.Common.Controllers;
    using Areas.Common.ViewModels.Home;
    using Data.Models;
    using Infrastructure.Mapping;
    using Moq;
    using Services.Data;
    using Services.Web;

    public class ProductsControllerTestsBase
    {
        private string productDescription = "SomeDescr";
        private ProductsController controller;

        public ProductsControllerTestsBase()
        {
            this.Controller = this.SetupController();
        }

        protected string ProductDescription
        {
            get { return this.productDescription; }
        }

        protected ProductsController Controller
        {
            get { return this.controller; }
            private set { this.controller = value; }
        }

        private ProductsController SetupController()
        {
            //const string ProductDescription = "SomeDescr";
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(ProductsController).Assembly);
            var productsServiceMock = new Mock<IProductsService>();
            productsServiceMock
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new Product()
                {
                    Title = "someName",
                    ShortDescription = this.ProductDescription,
                    Category = new Category()
                    {
                        Name = "someName"
                    }
                });

            var cacheServiceMock = new Mock<ICacheService>();
            var controller = new ProductsController(productsServiceMock.Object, cacheServiceMock.Object);

            return controller;
        }
    }
}

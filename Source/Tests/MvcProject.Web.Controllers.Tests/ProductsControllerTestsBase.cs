namespace MvcProject.Web.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Areas.Common.Controllers;
    using Data.Models;
    using Infrastructure.Mapping;
    using Moq;
    using Services.Data;

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
                        Name = "someName",
                        Description = this.ProductDescription,
                        Category = new Category()
                        {
                            Name = "someName"
                        }
                    });

            var controller = new ProductsController(productsServiceMock.Object);

            return controller;
        }
    }
}

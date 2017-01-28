namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Areas.Public.Controllers;
    using Areas.Public.ViewModels.Orders;
    using Areas.Public.ViewModels.Products;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data;
    using Services.Web;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class OrdersControllerTests
    {
        private Mock<IIdentifierProvider> identifierProvider;
        private Mock<IOrderItemsService> orderItemsService;
        private Mock<IOrdersService> ordersService;
        private Mock<IProductsService> productsService;
        private Mock<IMappingService> mappingService;

        public OrdersControllerTests()
        {
            this.PrepareController();
        }

        [Test]
        public void ShoppingCartWithExistingCartInSessionReturnsCorrectly()
        {
            // Arrange
            var userName = "jkhfjksdf";
            var cart = this.GetShoppingCart(userName);

            var identity = new Mock<IIdentity>();
            identity.Setup(x => x.Name).Returns(userName);
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(p => p.Identity).Returns(identity.Object);

            var session = new Mock<HttpSessionStateBase>();
            session.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);

            // Act
            var controller = new OrdersController(this.identifierProvider.Object, this.orderItemsService.Object, this.ordersService.Object, this.productsService.Object, this.mappingService.Object);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(x => x.HttpContext.Session).Returns(session.Object);
            controller.ControllerContext = controllerContext.Object;

            // Assert
            controller.WithCallTo(x => x.ShoppingCart())
                .ShouldRenderDefaultView()
                .WithModel<ShoppingCartViewModel>(vm => 
                {
                    Assert.IsNotNull(vm);
                    Assert.AreEqual(vm, cart);
                    Assert.AreEqual(vm.TotalCost, cart.TotalCost);
                    Assert.AreEqual(vm.UserName, cart.UserName);
                    CollectionAssert.AllItemsAreNotNull(vm.CartItems);
                    Assert.AreEqual(vm.CartItems.Count, cart.CartItems.Count);
                    CollectionAssert.AreEquivalent(vm.CartItems, cart.CartItems);
                    CollectionAssert.AreEquivalent(vm.CartItems.Select(x => x.Product), cart.CartItems.Select(x => x.Product));
                });
        }

        [Test]
        public void ShoppingCartWithoutExistingCartInSessionReturnsCorrectly()
        {
            // Arrange
            var userName = "jkhfjksdf";
            var cart = this.GetShoppingCart(userName);
            cart.TotalCost = default(decimal);
            //var sessionKey = "jkghkhsfjhs";

            var principal = this.GetPrincipalMock(userName);
            var controllerContext = new Mock<ControllerContext>();

            var session = new Mock<HttpSessionStateBase>();
            session.SetupGet(x => x[It.IsAny<string>()]).Returns(null);
            session.SetupSet(x => { x[It.IsAny<string>()] = cart; });

            // Act
            var controller = new OrdersController(this.identifierProvider.Object, this.orderItemsService.Object, this.ordersService.Object, this.productsService.Object, this.mappingService.Object);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(x => x.HttpContext.Session).Returns(session.Object);
            controller.ControllerContext = controllerContext.Object;

            // Assert
            controller.WithCallTo(x => x.ShoppingCart())
                .ShouldRenderDefaultView()
                .WithModel<ShoppingCartViewModel>(vm =>
                {
                    Assert.IsNotNull(vm);
                    CollectionAssert.AllItemsAreNotNull(vm.CartItems);
                    Assert.AreEqual(vm.TotalCost, cart.TotalCost);
                    Assert.AreEqual(vm.UserName, cart.UserName);
                });
        }

        [Test]
        public void UpdateShoppingCartWorksCorrectlyWhenModelAndModelStateAreValid()
        {
            // Arrange
            var cart = this.GetShoppingCart("jkfhsjd");
            var session = new Mock<HttpSessionStateBase>();
            session.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);
            session.SetupSet(x => { x[It.IsAny<string>()] = cart; });
            var controllerContext = new Mock<ControllerContext>();

            // Act
            var controller = new OrdersController(this.identifierProvider.Object, this.orderItemsService.Object, this.ordersService.Object, this.productsService.Object, this.mappingService.Object);
            controllerContext.SetupGet(x => x.HttpContext.Session).Returns(session.Object);
            controller.ControllerContext = controllerContext.Object;

            // Assert
            controller.WithCallTo(x => x.UpdateShoppingCart(cart))
                .ShouldRenderPartialView("ShoppingCartPartial")
                .WithModel<ShoppingCartViewModel>(vm => 
                {
                    Assert.IsNotNull(vm);
                    Assert.AreEqual(vm, cart);
                    Assert.AreEqual(vm.TotalCost, cart.TotalCost);
                    Assert.AreEqual(vm.UserName, cart.UserName);
                    CollectionAssert.AllItemsAreNotNull(vm.CartItems);
                    CollectionAssert.AreEquivalent(vm.CartItems, cart.CartItems);
                    Assert.IsTrue(vm.CartItems.All(x => x.ToDelete == false && x.ProductQuantity > 0));
                });
        }

        [Test]
        public void UpdateShoppingCartThrowsWhenModelIsInValid()
        {
            // Act
            var controller = new OrdersController(this.identifierProvider.Object, this.orderItemsService.Object, this.ordersService.Object, this.productsService.Object, this.mappingService.Object);
            
            // Assert
            var ex = Assert.Throws<HttpException>(() => controller.UpdateShoppingCart(null));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ex.GetHttpCode());
        }

        [Test]
        public void UpdateShoppingCartThrowsWhenModelStateIsInValid()
        {
            // Arrange
            var cart = this.GetShoppingCart("jkfhsjd");

            // Act
            var controller = new OrdersController(this.identifierProvider.Object, this.orderItemsService.Object, this.ordersService.Object, this.productsService.Object, this.mappingService.Object);
            controller.ModelState.AddModelError(string.Empty, It.IsAny<string>());

            // Assert
            var ex = Assert.Throws<HttpException>(() => controller.UpdateShoppingCart(cart));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ex.GetHttpCode());
        }

        [Test]
        public void AddToCartGetActionWorksCorrectly()
        {
            // Arrange
            var encodedProductId = "jkhkuqlxiooueujfukwh";
            var decodedProductId = 32;
            this.identifierProvider.Setup(x => x.EncodeIntId(It.IsAny<int>())).Returns(encodedProductId);
            this.identifierProvider.Setup(x => x.DecodeToIntId(It.IsAny<string>())).Returns(decodedProductId);
            this.mappingService.Setup(x => x.Map<ProductForShoppingCart>(It.IsAny<Product>())).Returns(new ProductForShoppingCart());
            this.productsService.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Product());
            var userName = "kkkqjquuuu";
            var cart = this.GetShoppingCart(userName);
            var principal = this.GetPrincipalMock(userName);
            var session = new Mock<HttpSessionStateBase>();
            session.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);
            session.SetupSet(x => { x[It.IsAny<string>()] = cart; });
            var controllerContext = new Mock<ControllerContext>();

            // Act
            var controller = new OrdersController(this.identifierProvider.Object, this.orderItemsService.Object, this.ordersService.Object, this.productsService.Object, this.mappingService.Object);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(x => x.HttpContext.Session).Returns(session.Object);
            controller.ControllerContext = controllerContext.Object;

            // Assert
            controller.WithCallTo(x => x.AddToCart(encodedProductId))
                .ShouldRedirectTo(x => x.ShoppingCart);
        }

        [Test]
        public void AddToCartPostActionWorksCorrectlyWhenProductIdAndModelStateAreValid()
        {
            // Arrange
            var encodedProductId = "jkhkuqlxiooueujfukwh";
            var decodedProductId = 32;
            var quantity = 3;
            this.identifierProvider.Setup(x => x.EncodeIntId(It.IsAny<int>())).Returns(encodedProductId);
            this.identifierProvider.Setup(x => x.DecodeToIntId(It.IsAny<string>())).Returns(decodedProductId);
            this.mappingService.Setup(x => x.Map<ProductForShoppingCart>(It.IsAny<Product>())).Returns(new ProductForShoppingCart());
            this.productsService.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Product());
            var userName = "kkkqjquuuu";
            var cart = this.GetShoppingCart(userName);
            var principal = this.GetPrincipalMock(userName);
            var session = new Mock<HttpSessionStateBase>();
            session.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);
            session.SetupSet(x => { x[It.IsAny<string>()] = cart; });
            var controllerContext = new Mock<ControllerContext>();

            // Act
            var controller = new OrdersController(this.identifierProvider.Object, this.orderItemsService.Object, this.ordersService.Object, this.productsService.Object, this.mappingService.Object);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(x => x.HttpContext.Session).Returns(session.Object);
            controller.ControllerContext = controllerContext.Object;

            // Assert
            controller.WithCallTo(x => x.AddToCart(encodedProductId, quantity))
                .ShouldRedirectTo(x => x.ShoppingCart);
        }

        [Test]
        public void AddToCartPostActionWorksCorrectlyWhenProductIdIsInValid()
        {
            // Arrange
            var encodedProductId = string.Empty;
            var quantity = 3;

            // Act
            var controller = new OrdersController(this.identifierProvider.Object, this.orderItemsService.Object, this.ordersService.Object, this.productsService.Object, this.mappingService.Object);

            // Assert
            controller.WithCallTo(x => x.AddToCart(encodedProductId, quantity))
                .ShouldRedirectTo(x => x.ShoppingCart);
        }

        [Test]
        public void AddToCartPostActionWorksCorrectlyWhenModelStateIsInValid()
        {
            // Arrange
            var encodedProductId = "jkkhapqphehf";
            var quantity = 3;
            var controllerContext = new Mock<ControllerContext>();

            // Act
            var controller = new OrdersController(this.identifierProvider.Object, this.orderItemsService.Object, this.ordersService.Object, this.productsService.Object, this.mappingService.Object);
            controller.ControllerContext = controllerContext.Object;
            controller.ModelState.AddModelError(string.Empty, It.IsAny<string>());

            // Assert
            controller.WithCallTo(x => x.AddToCart(encodedProductId, quantity))
                .ShouldRedirectTo(x => x.ShoppingCart);
        }

        #region Helpers
        private void PrepareController()
        {
            this.identifierProvider = new Mock<IIdentifierProvider>();
            this.orderItemsService = new Mock<IOrderItemsService>();
            this.ordersService = new Mock<IOrdersService>();
            this.productsService = new Mock<IProductsService>();
            this.mappingService = new Mock<IMappingService>();
        }

        private Mock<IPrincipal> GetPrincipalMock(string userName)
        {
            var principal = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();
            identity.Setup(x => x.Name).Returns(userName);
            principal.SetupGet(p => p.Identity).Returns(identity.Object);
            return principal;
        }

        private ShoppingCartViewModel GetShoppingCart(string userName)
        {
            return new ShoppingCartViewModel()
            {
                UserName = userName,
                CartItems = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem
                    {
                        Product = new ProductForShoppingCart { Id = 32, QuantityInStock = 176, Title = "kjsgjfhjksfhjkgkfsgd", UnitPrice = 42.33m },
                        ProductQuantity = 2,
                        ToDelete = false
                    },
                    new ShoppingCartItem
                    {
                        Product = new ProductForShoppingCart { Id = 3, QuantityInStock = 15, Title = "ij2bnkjdhkajhr", UnitPrice = 87.91m },
                        ProductQuantity = 12,
                        ToDelete = false
                    },
                    new ShoppingCartItem
                    {
                        Product = new ProductForShoppingCart { Id = 16, QuantityInStock = 6, Title = "jk ukshukfhk kwhkeh", UnitPrice = 142.01m },
                        ProductQuantity = 1,
                        ToDelete = false
                    },
                },
                TotalCost = 872m,
            };
        }
        #endregion
    }
}

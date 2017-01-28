namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Areas.Public.Controllers;
    using Areas.Public.ViewModels.Orders;
    using Areas.Public.ViewModels.Products;
    using Data.Models.Catalog;
    using Data.Models.Orders;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Moq;
    using NUnit.Framework;
    using Services.Data;
    using Services.Web;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class OrdersControllerTests
    {
        private AutoMapperConfig autoMapperConfig;
        private Mock<IIdentifierProvider> identifierProviderMock;
        private Mock<IOrderItemsService> orderItemsServiceMock;
        private Mock<IOrdersService> ordersServiceMock;
        private Mock<IProductsService> productsServiceMock;
        private Mock<IMappingService> mappingServiceMock;

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
            var principalMock = this.GetPrincipalMock(userName);
            var sessionMock = new Mock<HttpSessionStateBase>();
            sessionMock.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);
            controllerContextMock.SetupGet(x => x.HttpContext.Session).Returns(sessionMock.Object);

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

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
            var principalMock = this.GetPrincipalMock(userName);
            var sessionMock = new Mock<HttpSessionStateBase>();
            sessionMock.SetupGet(x => x[It.IsAny<string>()]).Returns(null);
            sessionMock.SetupSet(x => { x[It.IsAny<string>()] = cart; });
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);
            controllerContextMock.SetupGet(x => x.HttpContext.Session).Returns(sessionMock.Object);

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

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
            var sessionMock = new Mock<HttpSessionStateBase>();
            sessionMock.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);
            sessionMock.SetupSet(x => { x[It.IsAny<string>()] = cart; });
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.Session).Returns(sessionMock.Object);

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

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
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            
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
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
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
            var userName = "kkkqjquuuu";
            var cart = this.GetShoppingCart(userName);
            this.identifierProviderMock.Setup(x => x.EncodeIntId(It.IsAny<int>())).Returns(encodedProductId);
            this.identifierProviderMock.Setup(x => x.DecodeToIntId(It.IsAny<string>())).Returns(decodedProductId);
            this.mappingServiceMock.Setup(x => x.Map<ProductForShoppingCart>(It.IsAny<Product>())).Returns(new ProductForShoppingCart());
            this.productsServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Product());
            var principalMock = this.GetPrincipalMock(userName);
            var sessionMock = new Mock<HttpSessionStateBase>();
            sessionMock.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);
            sessionMock.SetupSet(x => { x[It.IsAny<string>()] = cart; });
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);
            controllerContextMock.SetupGet(x => x.HttpContext.Session).Returns(sessionMock.Object);

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.AddToCart(encodedProductId))
                .ShouldRedirectTo(x => x.ShoppingCart());
        }

        [Test]
        public void AddToCartPostActionWorksCorrectlyWhenProductIdAndModelStateAreValid()
        {
            // Arrange
            var encodedProductId = "jkhkuqlxiooueujfukwh";
            var decodedProductId = 32;
            var quantity = 3;
            this.identifierProviderMock.Setup(x => x.EncodeIntId(It.IsAny<int>())).Returns(encodedProductId);
            this.identifierProviderMock.Setup(x => x.DecodeToIntId(It.IsAny<string>())).Returns(decodedProductId);
            this.mappingServiceMock.Setup(x => x.Map<ProductForShoppingCart>(It.IsAny<Product>())).Returns(new ProductForShoppingCart());
            this.productsServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Product());
            var userName = "kkkqjquuuu";
            var cart = this.GetShoppingCart(userName);
            var sessionMock = new Mock<HttpSessionStateBase>();
            sessionMock.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);
            sessionMock.SetupSet(x => { x[It.IsAny<string>()] = cart; });
            var principalMock = this.GetPrincipalMock(userName);
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);
            controllerContextMock.SetupGet(x => x.HttpContext.Session).Returns(sessionMock.Object);

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.AddToCart(encodedProductId, quantity))
                .ShouldRedirectTo(x => x.ShoppingCart());
        }

        [Test]
        public void AddToCartPostActionWorksCorrectlyWhenProductIdIsInValid()
        {
            // Arrange
            var encodedProductId = string.Empty;
            var quantity = 3;

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);

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

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ModelState.AddModelError(string.Empty, It.IsAny<string>());

            // Assert
            controller.WithCallTo(x => x.AddToCart(encodedProductId, quantity))
                .ShouldRedirectTo(x => x.ShoppingCart());
        }

        [Test]
        public void CheckoutWorksCorrectlyIfModelStateAndShoppingCartAreValid()
        {
            // Arrange
            var userName = "iohkahjkfhsdf";
            var principalMock = this.GetPrincipalMock(userName);
            var cart = this.GetShoppingCart(userName);
            this.productsServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Product());
            this.ordersServiceMock.Setup(x => x.Insert(It.IsAny<Order>()));
            this.orderItemsServiceMock.Setup(x => x.Update(It.IsAny<OrderItem>()));
            var sessionMock = new Mock<HttpSessionStateBase>();
            sessionMock.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);
            sessionMock.SetupSet(x => { x[It.IsAny<string>()] = cart; });
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);
            controllerContextMock.SetupGet(x => x.HttpContext.Session).Returns(sessionMock.Object);

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.Checkout())
                .ShouldRenderView("CheckoutSuccess")
                .WithModel<ShoppingCartViewModel>(vm => 
                {
                    Assert.IsNotNull(vm);
                    Assert.AreEqual(vm, cart);
                    CollectionAssert.AreEqual(vm.CartItems, cart.CartItems);
                    CollectionAssert.AreEqual(vm.CartItems.Select(x => x.Product), cart.CartItems.Select(x => x.Product));
                });
        }

        [Test]
        public void CheckoutShouldRedirectIfShoppingCartIsNull()
        {
            // Arrange
            var userName = "iohkahjkfhsdf";
            var cart = this.GetShoppingCart(userName);
            var principalMock = this.GetPrincipalMock(userName);
            var sessionMock = new Mock<HttpSessionStateBase>();
            sessionMock.SetupGet(x => x[It.IsAny<string>()]).Returns(null);
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);
            controllerContextMock.SetupGet(x => x.HttpContext.Session).Returns(sessionMock.Object);

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.Checkout())
                .ShouldRedirectTo(x => x.ShoppingCart());
        }

        [Test]
        public void CheckoutShouldRedirectIfShoppingCartIsEmpty()
        {
            // Arrange
            var userName = "iohkahjkfhsdf";
            var cart = this.GetShoppingCart(userName);
            cart.CartItems.Clear();
            var principalMock = this.GetPrincipalMock(userName);
            var sessionMock = new Mock<HttpSessionStateBase>();
            sessionMock.SetupGet(x => x[It.IsAny<string>()]).Returns(cart);
            sessionMock.SetupSet(x => { x[It.IsAny<string>()] = cart; });
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);
            controllerContextMock.SetupGet(x => x.HttpContext.Session).Returns(sessionMock.Object);

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.Checkout())
                .ShouldRedirectTo(x => x.ShoppingCart());
        }

        [Test]
        public void CheckoutShouldThrowIfModelStateIsInValid()
        {
            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controller.ModelState.AddModelError(string.Empty, It.IsAny<string>());

            // Assert
            var ex = Assert.Throws<HttpException>(() => controller.Checkout());
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ex.GetHttpCode());
        }

        [Test]
        public void OrderDetailsShouldReturnCorrectlyIfOrderIsNotNull()
        {
            // Arrange
            var userName = "jkhkahavvghio";
            var userId = "uyuihfuyuyunynuwgueyng";
            var orderRefNumber = Guid.NewGuid();
            var mockDbOrders = this.GetMockDbOrders();
            mockDbOrders.Add(new Order { RefNumber = orderRefNumber, UserId = userId });
            var viewModel = new OrderForUserProfile();
            this.ordersServiceMock.Setup(x => x.GetAll()).Returns(mockDbOrders.AsQueryable);
            this.mappingServiceMock.Setup(x => x.Map<OrderForUserProfile>(It.IsAny<Order>())).Returns(viewModel);
            this.productsServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Product());
            var principalMock = this.GetPrincipalMockWithClaimsIdentityMock(userName, userId);
            var controllerContextMock = new Mock<ControllerContext>();

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.OrderDetails(orderRefNumber))
                .ShouldRenderDefaultView()
                .WithModel<OrderForUserProfile>(vm =>
                {
                    Assert.IsNotNull(vm);
                    Assert.AreEqual(vm, viewModel);
                })
                ;
        }

        [Test]
        public void OrderDetailsShouldRedirectIfOrderIsNull()
        {
            // Arrange
            var userName = "jkhkahavvghio";
            var userId = "uyuihfuyuyunynuwgueyng";
            var orderRefNumber = Guid.NewGuid();
            var mockDbOrders = this.GetMockDbOrders();
            var viewModel = new OrderForUserProfile();
            this.ordersServiceMock.Setup(x => x.GetAll()).Returns(mockDbOrders.AsQueryable);
            this.mappingServiceMock.Setup(x => x.Map<OrderForUserProfile>(It.IsAny<Order>())).Returns(viewModel);
            var principalMock = this.GetPrincipalMockWithClaimsIdentityMock(userName, userId);
            var controllerContextMock = new Mock<ControllerContext>();

            // Act
            var controller = new OrdersController(this.identifierProviderMock.Object, this.orderItemsServiceMock.Object, this.ordersServiceMock.Object, this.productsServiceMock.Object, this.mappingServiceMock.Object);
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.OrderDetails(orderRefNumber))
                .ValidateActionReturnType<RedirectToRouteResult>();

            controller.WithCallTo(x => x.OrderDetails(orderRefNumber))
                .ShouldRedirectTo<UsersController>(x => x.OrderHistory());
        }

        #region Helpers
        private void PrepareController()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(OrdersController).Assembly);
            this.identifierProviderMock = new Mock<IIdentifierProvider>();
            this.orderItemsServiceMock = new Mock<IOrderItemsService>();
            this.ordersServiceMock = new Mock<IOrdersService>();
            this.productsServiceMock = new Mock<IProductsService>();
            this.mappingServiceMock = new Mock<IMappingService>();
        }

        private Mock<IPrincipal> GetPrincipalMock(string userName)
        {
            var principalMock = new Mock<IPrincipal>();
            var identityMock = new Mock<IIdentity>();
            identityMock.Setup(x => x.Name).Returns(userName);
            principalMock.SetupGet(p => p.Identity).Returns(identityMock.Object);
            return principalMock;
        }

        // Approach used: http://stackoverflow.com/a/23960592/4491770
        private Mock<IPrincipal> GetPrincipalMockWithClaimsIdentityMock(string userName, string userId)
        {
            var claim = new Claim(ClaimTypes.NameIdentifier, userId);
            var principalMock = new Mock<IPrincipal>();
            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(x => x.FindFirst(ClaimTypes.NameIdentifier)).Returns(claim);
            principalMock.SetupGet(p => p.Identity).Returns(/*identityMock*/ identity.Object);
            return principalMock;
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

        private List<Order> GetMockDbOrders()
        {
            return new List<Order>()
            {
                new Order { RefNumber = Guid.NewGuid(), UserId = "kguibiqbksdjhf" },
                new Order { RefNumber = Guid.NewGuid(), UserId = "qwwwhkhniwuegrjh" },
                new Order { RefNumber = Guid.NewGuid(), UserId = "ofhqiowur" },
            };
        }
        #endregion
    }
}

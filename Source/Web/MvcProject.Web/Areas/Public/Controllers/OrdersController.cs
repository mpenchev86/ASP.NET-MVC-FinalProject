namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models.Catalog;
    using ViewModels.Products;
    using Services.Data;
    using ViewModels.Orders;
    using Common.GlobalConstants;
    using Infrastructure.Authorization;
    using Data.Models.Orders;
    using Infrastructure.Extensions;

    [AuthorizeRoles(IdentityRoles.Customer, IdentityRoles.Seller)]
    public class OrdersController : BasePublicController
    {
        private readonly IUsersService usersService;
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;
        private readonly IOrderItemsService orderItemsService;

        public OrdersController(
            IUsersService usersService,
            IProductsService productsService,
            IOrdersService ordersService,
            IOrderItemsService orderItemsService)
        {
            this.usersService = usersService;
            this.productsService = productsService;
            this.ordersService = ordersService;
            this.orderItemsService = orderItemsService;
        }

        [HttpGet]
        public ActionResult ShoppingCart(/*ShoppingCartViewModel shoppingCart*/)
        {
            //var viewModel = new ShoppingCartViewModel()
            //{
            //    UserName = this.HttpContext.User.Identity.Name,

            //};

            var shoppingCart = new ShoppingCartViewModel();
            var userName = this.HttpContext.User.Identity.Name;
            var sessionKey = string.Format("{0}-{1}", userName, "ShoppingCart");

            if (this.HttpContext.Session.IsNewSession)
            {
                var user = this.usersService.GetByUserName(userName);
                //var sessionKey = string.Format("{0}-{1}", userId, "ShoppingCart");
                shoppingCart.UserId = user.Id;
                shoppingCart.UserName = userName;
                this.Session[sessionKey] = shoppingCart;
            }
            else
            {
                shoppingCart = (ShoppingCartViewModel)this.Session[sessionKey];
            }

            //shoppingCart.UserId = this.usersService.GetByUserName(shoppingCart.UserName).Id;

            return this.View(/*viewModel*/shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateShoppingCart(ShoppingCartViewModel shoppingCart)
        {
            if (shoppingCart != null && this.ModelState.IsValid)
            {
                shoppingCart.CartItems = shoppingCart.CartItems.Where(ci => ci.ToDelete == false).ToList();

                var sessionKey = string.Format("{0}-{1}", shoppingCart.UserName, "ShoppingCart");
                this.Session[sessionKey] = shoppingCart;
                return this.PartialView("ShoppingCartPartial", shoppingCart);
            }

            throw new HttpException(400, "Invalid shopping cart state.");
        }
        
        public ActionResult AddToCart(int productId)
        {
            if (this.ModelState.IsValid)
            {
                var userName = this.User.Identity.Name;
                var sessionKey = string.Format("{0}-{1}", userName, "ShoppingCart");
                var shoppingCart = (ShoppingCartViewModel)this.Session[sessionKey];

                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCartViewModel() { UserName = userName, UserId = this.usersService.GetByUserName(userName).Id };
                }

                var cartItem = shoppingCart.CartItems.FirstOrDefault(ci => ci.Product.Id == productId);
                if (cartItem != null)
                {
                    cartItem.ProductQuantity += 1;
                }
                else
                {
                    cartItem = new ShoppingCartItem();
                    cartItem.Product = PopulateProductForCartItem(this.productsService.GetById(productId));
                    cartItem.ProductQuantity = 1;
                    cartItem.ToDelete = false;
                    shoppingCart.CartItems.Add(cartItem);
                }

                this.Session[sessionKey] = shoppingCart;
            }

            return this.RedirectToAction("ShoppingCart");
        }

        public ActionResult Checkout()
        {
            var userName = this.User.Identity.Name;
            var sessionKey = string.Format("{0}-{1}", userName, "ShoppingCart");
            var shoppingCart = (ShoppingCartViewModel)this.Session[sessionKey];

            var order = new Order();
            PopulateOrder(order, shoppingCart);
            this.ordersService.Insert(order);
            var orderId = order.Id;
            PopulateOrderItems(order, shoppingCart.CartItems);
            this.ordersService.Update(order);

            //order.OrderItems = shoppingCart.CartItems.AsQueryable().To<OrderItem>().ToList();

            return this.View("CheckoutSuccess");
        }

        private void PopulateOrderItems(/*ICollection<OrderItem> orderItems*/Order order, ICollection<ShoppingCartItem> cartItems)
        {
            var orderItems = cartItems.AsQueryable().To<OrderItem>();
            foreach (var item in orderItems)
            {
                item.Product = this.productsService.GetById(item.ProductId);
                item.OrderId = order.Id;
                item.Order = this.ordersService.GetById(item.OrderId);
                item.IsDeleted = false;
                item.ModifiedOn = null;
                this.orderItemsService.Insert(item);
            }
        }

        private void PopulateOrder(Order order, ShoppingCartViewModel shoppingCart)
        {
            order.TotalCost = shoppingCart.TotalCost;
            order.UserId = shoppingCart.UserId;
            order.IsDeleted = false;
            order.ModifiedOn = null;
        }

        #region Helpers
        private ProductForShoppingCart PopulateProductForCartItem(Product product)
        {
            return new ProductForShoppingCart
            {
                Id = product.Id,
                Title = product.Title,
                UnitPrice = product.UnitPrice,
                ShippingPrice = product.ShippingPrice,
                ImageUrlPath = product.MainImageId != null ? product.MainImage.UrlPath  : (product.Images.Any() ? product.Images.FirstOrDefault().UrlPath : ""),
                ImageFileExtension = product.MainImageId != null ? product.MainImage.FileExtension  : (product.Images.Any() ? product.Images.FirstOrDefault().FileExtension : ""),
            };
        }
        #endregion
    }
}
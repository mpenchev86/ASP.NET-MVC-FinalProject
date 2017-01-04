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
    using Infrastructure.Filters;
    using Data.Models.Orders;
    using Infrastructure.Extensions;
    using Microsoft.AspNet.Identity;

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
        public ActionResult ShoppingCart()
        {
            var shoppingCart = new ShoppingCartViewModel();
            var userName = this.HttpContext.User.Identity.Name;
            var sessionKey = GetSessionKey(userName);

            if (this.Session[sessionKey] == null)
            {
                shoppingCart.UserName = userName;
                this.Session[sessionKey] = shoppingCart;
            }
            else
            {
                shoppingCart = (ShoppingCartViewModel)this.Session[sessionKey];
            }

            return this.View(shoppingCart);
        }

        [ChildActionOnly]
        public /*ActionResult*/PartialViewResult GetShoppingCartPartial()
        {
            var sessionKey = GetSessionKey(this.User.Identity.Name);
            var shoppingCart = (ShoppingCartViewModel)this.Session[sessionKey];
            return this.PartialView("ShoppingCartPartial", shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateShoppingCart(ShoppingCartViewModel shoppingCart)
        {
            if (shoppingCart != null && this.ModelState.IsValid)
            {
                shoppingCart.CartItems = shoppingCart.CartItems.Where(ci => ci.ToDelete == false && ci.ProductQuantity > 0).ToList();
                shoppingCart.TotalCost = shoppingCart.CartItems.Aggregate(0m, (itemCost, item) => itemCost + (item.Product.UnitPrice * item.ProductQuantity));
                var sessionKey = GetSessionKey(shoppingCart.UserName);
                this.Session[sessionKey] = shoppingCart;

                // Refreshes the model state so that html helpers receive correct values. Info: https://blogs.msdn.microsoft.com/simonince/2010/05/05/asp-net-mvcs-html-helpers-render-the-wrong-value/
                this.ModelState.Clear();

                return this.PartialView("ShoppingCartPartial", shoppingCart);
            }

            throw new HttpException(400, "Invalid shopping cart state.");
        }
        
        public ActionResult AddToCart(int productId)
        {
            if (this.ModelState.IsValid)
            {
                var userName = this.User.Identity.Name;
                var sessionKey = GetSessionKey(userName);
                var shoppingCart = (ShoppingCartViewModel)this.Session[sessionKey];

                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCartViewModel() { UserName = userName };
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
                    shoppingCart.TotalCost += cartItem.Product.UnitPrice * cartItem.ProductQuantity;
                }

                this.Session[sessionKey] = shoppingCart;
            }

            return this.RedirectToAction("ShoppingCart");
        }

        public ActionResult Checkout()
        {
            if (this.ModelState.IsValid)
            {
                var userName = this.User.Identity.Name;
                var sessionKey = GetSessionKey(userName);
                var shoppingCart = (ShoppingCartViewModel)this.Session[sessionKey];

                if (shoppingCart == null)
                {
                    return this.RedirectToAction("ShoppingCart");
                }

                if (!shoppingCart.CartItems.Any())
                {
                    return this.RedirectToAction("ShoppingCart");
                }

                var order = new Order();
                PopulateOrder(order, shoppingCart);
                this.ordersService.Insert(order);
                var orderId = order.Id;
                PopulateOrderItems(order, shoppingCart.CartItems);
                this.ordersService.Update(order);
                this.Session[sessionKey] = null;

                return this.View("CheckoutSuccess");
            }

            throw new HttpException(400, "Invalid checkout request.");
        }

        private void PopulateOrderItems(Order order, ICollection<ShoppingCartItem> cartItems)
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
            order.UserId = this.User.Identity.GetUserId();
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

        private string GetSessionKey(string userName)
        {
            return string.Format("{0}-{1}", userName, "ShoppingCart");
        }
        #endregion
    }
}
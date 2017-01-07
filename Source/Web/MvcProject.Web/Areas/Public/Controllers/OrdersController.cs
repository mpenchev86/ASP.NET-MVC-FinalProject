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
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
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

        // Preserving ModelState across PRG: http://www.jefclaes.be/2012/06/persisting-model-state-when-using-prg.html
        [HttpGet]
        [RestoreModelStateFromTempData]
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
        
        [HttpGet]
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

        [HttpGet]
        [SetTempDataModelState]
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
                    this.ModelState.AddModelError(string.Empty, "Add something to your cart before checkout.");
                    return this.RedirectToAction("ShoppingCart");
                }

                if (!ValidateItemQuantities(shoppingCart.CartItems.ToList(), this.ModelState))
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

                return this.View("CheckoutSuccess", shoppingCart);
            }

            throw new HttpException(400, "Invalid checkout request.");
        }

        #region Helpers
        private void PopulateOrder(Order order, ShoppingCartViewModel shoppingCart)
        {
            order.TotalCost = shoppingCart.TotalCost;
            order.UserId = this.User.Identity.GetUserId();
            //order.IsDeleted = false;
            //order.ModifiedOn = null;
        }

        private void PopulateOrderItems(Order order, ICollection<ShoppingCartItem> cartItems)
        {
            var orderItems = cartItems.AsQueryable().To<OrderItem>();
            foreach (var item in orderItems)
            {
                item.Product = this.productsService.GetById(item.ProductId);
                item.OrderId = order.Id;
                item.Order = this.ordersService.GetById(item.OrderId);
                //item.IsDeleted = false;
                //item.ModifiedOn = null;
                this.orderItemsService.Insert(item);
            }
        }

        private ProductForShoppingCart PopulateProductForCartItem(Product product)
        {
            return new ProductForShoppingCart
            {
                Id = product.Id,
                Title = product.Title,
                UnitPrice = product.UnitPrice,
                QuantityInStock = product.QuantityInStock,
                ShippingPrice = product.ShippingPrice,
                ImageUrlPath = product.MainImageId != null ? product.MainImage.UrlPath  : (product.Images.Any() ? product.Images.FirstOrDefault().UrlPath : ""),
                ImageFileExtension = product.MainImageId != null ? product.MainImage.FileExtension  : (product.Images.Any() ? product.Images.FirstOrDefault().FileExtension : ""),
            };
        }

        private string GetSessionKey(string userName)
        {
            return string.Format("{0}-{1}", userName, "ShoppingCart");
        }

        private bool ValidateItemQuantities(List<ShoppingCartItem> cartItems, ModelStateDictionary modelState)
        {
            for (int i = 0; i < cartItems.Count(); i++)
            {
                try
                {
                    var productInDb = this.productsService.GetById(cartItems[i].Product.Id);
                    productInDb.QuantityInStock -= cartItems[i].ProductQuantity;
                    this.productsService.Update(productInDb);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Product)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save changes. The following product was not found: " + cartItems[i].Product.Title);
                    }
                    else
                    {
                        var databaseValues = (Product)databaseEntry.ToObject();
                        if (databaseValues.QuantityInStock < clientValues.QuantityInStock)
                        {
                            var key = "CartItems[" + i.ToString() + "].ProductQuantity";
                            var message = "The requested quantity exceeds the available number of units for product " + cartItems[i].Product.Title + ".";
                            modelState.AddModelError(key, message);
                        }
                    }

                }
                catch (DbEntityValidationException ex)
                {
                    HandleExceededQuantityException(ex, modelState, i);
                }


                //if (cartItems[i].ProductQuantity > cartItems[i].Product.QuantityInStock)
                //{
                //    var key = "CartItems[" + i.ToString() + "].ProductQuantity";
                //    var message = "The requested quantity exceeds the available number of units for product " + cartItems[i].Product.Title + ".";
                //    modelState.AddModelError(key, message);
                //}
            }

            if (modelState.IsValid)
            {
                return true;
            }

            return false;
        }

        private void HandleExceededQuantityException(DbEntityValidationException ex, ModelStateDictionary modelState, int cartItemIndex)
        {
            //((DbEntityValidationException)inner).EntityValidationErrors

            if (!ex.EntityValidationErrors.Any())
            {
                //var inner = ex.InnerException;
                this.HandleExceededQuantityException((DbEntityValidationException)ex.InnerException, modelState, cartItemIndex);
            }
            else
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    var productEntity = (Product)error.Entry.Entity;
                    if (productEntity.QuantityInStock < 0)
                    {
                        var key = "CartItems[" + cartItemIndex.ToString() + "].ProductQuantity";
                        var message = "The requested quantity exceeds the available number of units for product " + productEntity.Title + ".";
                        modelState.AddModelError(key, message);
                        break;
                    }
                }
            }

            //throw new NotImplementedException();
        }
        #endregion
    }
}
﻿namespace JustOrderIt.Web.Areas.Public.Controllers
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
    using Services.Web;
    using Infrastructure.Mapping;
    using Infrastructure.Validators;
    using ViewModels.Votes;

    [AuthorizeRoles(IdentityRoles.Customer, IdentityRoles.Seller)]
    public class OrdersController : BasePublicController
    {
        private readonly IIdentifierProvider identifierProvider;
        private readonly IOrderItemsService orderItemsService;
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly IMappingService mappingService;

        public OrdersController(
            IIdentifierProvider identifierProvider,
            IOrderItemsService orderItemsService,
            IOrdersService ordersService,
            IProductsService productsService,
            IMappingService mappingService)
        {
            this.identifierProvider = identifierProvider;
            this.orderItemsService = orderItemsService;
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.mappingService = mappingService;
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
        public ActionResult AddToCart(string productId)
        {
            if (!string.IsNullOrWhiteSpace(productId))
            {
                this.ProccessSessionCart(productId, 1);
            }

            return this.RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(string productId, int quantity = 1)
        {
            if (this.ModelState.IsValid && !string.IsNullOrWhiteSpace(productId))
            {
                this.ProccessSessionCart(productId, quantity);
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

        [HttpGet]
        public ActionResult OrderDetails(Guid refNumber)
        {
            var userId = this.User.Identity.GetUserId();
            var order = this.ordersService.GetAll().FirstOrDefault(o => o.RefNumber == refNumber && o.UserId == userId);
            if (order == null)
            {
                return this.RedirectToAction("OrderHistory", "Users");
            }

            var viewModel = this.mappingService.Map<OrderForUserProfile>(order);
            foreach (var itemProduct in viewModel.OrderItems.Select(oi => oi.Product))
            {
                var vote = this.productsService.GetById(itemProduct.Id).Votes.FirstOrDefault(v => v.UserId == order.UserId);
                itemProduct.Rating = vote?.VoteValue ?? 0;
            }

            return this.View(viewModel);
        }

        #region Helpers
        private void ProccessSessionCart(string productId, int quantity)
        {
            var userName = this.User.Identity.Name;
            var sessionKey = GetSessionKey(userName);
            var shoppingCart = (ShoppingCartViewModel)this.Session[sessionKey];

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCartViewModel() { UserName = userName };
            }

            var decodedId = (int)this.identifierProvider.DecodeToIntId(productId);
            var cartItem = shoppingCart.CartItems.FirstOrDefault(ci => ci.Product.Id == decodedId);
            if (cartItem != null)
            {
                cartItem.ProductQuantity += quantity;
            }
            else
            {
                cartItem = new ShoppingCartItem();
                cartItem.Product = this.mappingService.Map<ProductForShoppingCart>(this.productsService.GetById(decodedId));
                cartItem.ProductQuantity = quantity;
                cartItem.ToDelete = false;
                shoppingCart.CartItems.Add(cartItem);
                shoppingCart.TotalCost += cartItem.Product.UnitPrice * cartItem.ProductQuantity;
            }

            this.Session[sessionKey] = shoppingCart;
        }

        private void PopulateOrder(Order order, ShoppingCartViewModel shoppingCart)
        {
            order.RefNumber = Guid.NewGuid();
            order.TotalCost = shoppingCart.TotalCost;
            order.UserId = this.User.Identity.GetUserId();
        }

        private void PopulateOrderItems(Order order, ICollection<ShoppingCartItem> cartItems)
        {
            var orderItems = cartItems.AsQueryable().To<OrderItem>();
            foreach (var item in orderItems)
            {
                item.Product = this.productsService.GetById(item.ProductId);
                item.OrderId = order.Id;
                item.Order = this.ordersService.GetById(item.OrderId);
                this.orderItemsService.Insert(item);
            }
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
                    productInDb.AllTimeItemsSold += cartItems[i].ProductQuantity;
                    this.productsService.Update(productInDb);
                }
                catch (DbEntityValidationException ex)
                {
                    var key = "CartItems[" + i.ToString() + "].ProductQuantity";
                    var message = "The requested quantity exceeds the available number of units for product " + cartItems[i].Product.Title + ".";
                    HandleExceededQuantityException(ex, modelState, message);
                }
            }

            if (modelState.IsValid)
            {
                return true;
            }

            return false;
        }

        private void HandleExceededQuantityException(DbEntityValidationException ex, ModelStateDictionary modelState, string quantityExceededMessage)
        {
            if (!ex.EntityValidationErrors.Any())
            {
                this.HandleExceededQuantityException((DbEntityValidationException)ex.InnerException, modelState, quantityExceededMessage);
            }
            else
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    var productEntity = (Product)error.Entry.Entity;
                    if (productEntity.QuantityInStock < 0)
                    {
                        modelState.AddModelError(string.Empty, quantityExceededMessage);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
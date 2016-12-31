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

    public class OrdersController : BasePublicController
    {
        private readonly IUsersService usersService;
        private readonly IProductsService productsService;

        public OrdersController(IUsersService usersService, IProductsService productsService)
        {
            this.usersService = usersService;
            this.productsService = productsService;
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
                if (shoppingCart/*this.Session[sessionKey]*/ == null)
                {
                    /*this.Session[sessionKey]*/shoppingCart = new ShoppingCartViewModel() { UserName = userName, UserId = this.usersService.GetByUserName(userName).Id };
                }

                var cartItem = shoppingCart.CartItems.FirstOrDefault(ci => ci.Product.Id == productId) ?? new ShoppingCartItem();
                if (cartItem.Product != null)
                {
                    cartItem.ProductQuantity += 1;
                }
                else
                {
                    cartItem.Product = PopulateProductForCartItem(this.productsService.GetById(productId));
                    cartItem.ProductQuantity = 1;
                }

                this.Session[sessionKey] = shoppingCart;
            }

            return this.RedirectToAction("ShoppingCart");
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
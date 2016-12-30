namespace MvcProject.Web.Areas.Public.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Products;

    public class ShoppingCartItem
    {
        public int ProductQuantity { get; set; }

        public ProductForShoppingCart Product { get; set; }
    }
}
namespace MvcProject.Web.Areas.Public.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models.Orders;
    using Infrastructure.Mapping;
    using Products;

    public class ShoppingCartItem : /*BasePublicViewModel<int>,*/ IMapFrom<OrderItem>
    {
        public int ProductQuantity { get; set; }

        //public int OrderId { get; set; }

        //public ShoppingCartViewModel Order { get; set; }

        //public int ProductId { get; set; }

        public ProductForShoppingCart Product { get; set; }
    }
}
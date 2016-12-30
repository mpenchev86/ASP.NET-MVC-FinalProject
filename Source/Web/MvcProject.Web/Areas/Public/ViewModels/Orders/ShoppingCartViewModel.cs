namespace MvcProject.Web.Areas.Public.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Data.Models.Identity;
    using Data.Models.Orders;
    using Infrastructure.Mapping;

    public class ShoppingCartViewModel : BasePublicViewModel<int>, IMapFrom<Order>
    {
        private ICollection<ShoppingCartItem> cartItems;

        public ShoppingCartViewModel()
        {
            this.cartItems = new HashSet<ShoppingCartItem>();
        }

        //public decimal TotalCost { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ShoppingCartItem> CartItems
        {
            get { return this.cartItems; }
            set { this.cartItems = value; }
        }
    }
}
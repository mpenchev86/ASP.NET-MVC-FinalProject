namespace JustOrderIt.Web.Areas.Public.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using Data.Models.Orders;
    using Infrastructure.Mapping;
    using Products;

    public class OrderItemForUserProfile : BasePublicViewModel<int>, IMapFrom<OrderItem>
    {
        [Required]
        public int ProductQuantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual ProductForUserProfileOrder Product { get; set; }
    }
}
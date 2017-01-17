namespace JustOrderIt.Web.Areas.Public.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models.Orders;
    using Infrastructure.Mapping;

    public class OrderForUserProfile : BasePublicViewModel<int>, IMapFrom<Order>
    {
        private ICollection<OrderItemForUserProfile> orderItems;

        public OrderForUserProfile()
        {
            this.orderItems = new HashSet<OrderItemForUserProfile>();
        }

        public decimal TotalCost { get; set; }

        public virtual ICollection<OrderItemForUserProfile> OrderItems
        {
            get { return this.orderItems; }
            set { this.orderItems = value; }
        }
    }
}
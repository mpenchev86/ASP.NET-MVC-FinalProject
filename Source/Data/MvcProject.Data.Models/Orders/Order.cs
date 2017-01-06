namespace MvcProject.Data.Models.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catalog;
    using Contracts;
    using Identity;

    public class Order : BaseEntityModel<int>
    {
        private ICollection<OrderItem> orderItems;

        public Order()
        {
            this.orderItems = new HashSet<OrderItem>();
        }

        public decimal TotalCost { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<OrderItem> OrderItems
        {
            get { return this.orderItems; }
            set { this.orderItems = value; }
        }
    }
}

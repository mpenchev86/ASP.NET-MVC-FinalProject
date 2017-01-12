namespace JustOrderIt.Data.Models.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catalog;
    using Contracts;

    public class OrderItem : BaseEntityModel<int>
    {
        [Required]
        public int ProductQuantity { get; set; }

        [Required]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}

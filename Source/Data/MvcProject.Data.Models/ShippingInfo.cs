namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityContracts;

    [NotMapped]
    public class ShippingInfo : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Product> products;

        public ShippingInfo()
        {
            this.products = new HashSet<Product>();
        }

        [Range(0, int.MaxValue)]
        public int DaysToDelivery { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ShippingPrice { get; set; }

        public string Country { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
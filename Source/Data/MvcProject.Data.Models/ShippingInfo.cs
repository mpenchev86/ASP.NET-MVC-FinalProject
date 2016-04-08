namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityContracts;

    [NotMapped]
    public class ShippingInfo : BaseEntityModel<string>
    {
        private ICollection<Product> products;

        public ShippingInfo()
        {
            this.products = new HashSet<Product>();
        }

        //[Column(TypeName = "datetime2")]
        //public DateTime EarliestShippingDate { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime LatestShippingDate { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime EarliestDeliveryDate { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime LatestDeliveryDate { get; set; }

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
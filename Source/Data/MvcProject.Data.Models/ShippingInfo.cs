namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityContracts;

    public class ShippingInfo : BaseEntityModel<int>
    {
        public ShippingInfo()
        {
            this.ShippingCountries = new HashSet<string>();
        }

        [Column(TypeName = "datetime2")]
        public DateTime EarliestShippingDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LatestShippingDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EarliestDeliveryDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LatestDeliveryDate { get; set; }

        public HashSet<string> ShippingCountries { get; set; }
    }
}
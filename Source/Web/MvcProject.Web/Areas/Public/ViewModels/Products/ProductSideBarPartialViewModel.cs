namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using Services.Web;

    public class ProductSideBarPartialViewModel : BasePublicViewModel<int>
    {
        public string Title { get; set; }

        public string Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal? ShippingPrice { get; set; }
        
        public int QuantityInStock { get; set; }

        public bool IsInStock { get; set; }

        public string RefNumber { get; set; }

        public double Rating { get; set; }
    }
}
namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using Properties;
    using MvcProject.Data.Models;

    public class ProductInfoPartialViewModel
    {
        [DataType(DataType.MultilineText)]
        public string DescriptionContent { get; set; }

        [UIHint("DescriptionProperties")]
        public ICollection<PropertyForProductFullViewModel> DescriptionProperties { get; set; }

        [UIHint("ProductTags")]
        public IEnumerable<string> Tags { get; set; }
    }
}
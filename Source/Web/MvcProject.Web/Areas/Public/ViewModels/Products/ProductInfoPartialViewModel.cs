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
        private IEnumerable<PropertyForProductFullViewModel> descriptionProperties;
        private IEnumerable<string> tags;

        public ProductInfoPartialViewModel()
        {
            this.descriptionProperties = new HashSet<PropertyForProductFullViewModel>();
            this.tags = new HashSet<string>();
        }

        [DataType(DataType.MultilineText)]
        public string DescriptionContent { get; set; }

        [UIHint("DescriptionProperties")]
        public IEnumerable<PropertyForProductFullViewModel> DescriptionProperties
        {
            get { return this.descriptionProperties; }
            set { this.descriptionProperties = value; }
        }

        [UIHint("ProductTags")]
        public IEnumerable<string> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
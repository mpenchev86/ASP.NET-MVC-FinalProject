namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ProductImagesPartialViewModel
    {
        private ICollection<Image> images;

        public ProductImagesPartialViewModel()
        {
            this.images = new HashSet<Image>();
        }

        public int? MainImageId { get; set; }

        public ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
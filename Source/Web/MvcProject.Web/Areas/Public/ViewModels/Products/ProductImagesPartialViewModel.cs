namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Images;
    using Infrastructure.Mapping;

    public class ProductImagesPartialViewModel
    {
        private ICollection<ImageForProductFullViewModel> images;

        public ProductImagesPartialViewModel()
        {
            this.images = new HashSet<ImageForProductFullViewModel>();
        }

        public int? MainImageId { get; set; }

        public ICollection<ImageForProductFullViewModel> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
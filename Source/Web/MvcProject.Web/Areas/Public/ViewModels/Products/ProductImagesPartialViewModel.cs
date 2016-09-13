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
        
        public ImageForProductFullViewModel MainImage
        {
            get
            {
                return (this.Images != null ? this.images.FirstOrDefault(img => img.IsMainImage) : null);
            }
        }

        public ICollection<ImageForProductFullViewModel> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
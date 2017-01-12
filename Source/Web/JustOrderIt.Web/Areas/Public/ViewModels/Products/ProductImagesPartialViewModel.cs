namespace JustOrderIt.Web.Areas.Public.ViewModels.Products
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
                if (this.Images.Any())
                {
                    return this.images.FirstOrDefault(img => img.IsMainImage) ?? this.images.FirstOrDefault();
                }

                return new ImageForProductFullViewModel();
            }
        }

        public ICollection<ImageForProductFullViewModel> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
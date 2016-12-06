namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ProductForQuerySearchViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrlPath { get; set; }

        public string ImageFileExtension { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductForQuerySearchViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImageUrlPath, opt => opt.MapFrom(
                            //src => (src.Images != null && src.Images.Any()) ? (src.Images.FirstOrDefault(img => img.IsMainImage) ?? src.Images.FirstOrDefault()).UrlPath : ""))
                            src => src.MainImage != null ? src.MainImage.UrlPath : (src.Images.Any() ? src.Images.FirstOrDefault().UrlPath : "")))
                .ForMember(dest => dest.ImageFileExtension, opt => opt.MapFrom(
                            //src => (src.Images != null && src.Images.Any()) ? (src.Images.FirstOrDefault(img => img.IsMainImage) ?? src.Images.FirstOrDefault()).FileExtension : ""))
                            src => src.MainImage != null ? src.MainImage.FileExtension : (src.Images.Any() ? src.Images.FirstOrDefault().FileExtension : "")))
                ;
        }
    }
}
namespace JustOrderIt.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;
    using Services.Web;

    public class ProductForQuerySearchViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IMapFrom<ProductCacheViewModel>, IHaveCustomMappings
    {
        public string EncodedId
        {
            get { return IdentifierProvider.EncodeIntIdStatic(this.Id); }
        }

        public string Title { get; set; }

        public string ShortTitle
        {
            get
            {
                return this.Title.Length >= 35 ? this.Title.Substring(0, 35) + "..." : this.Title;
            }
        }

        public string ShortDescription { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrlPath { get; set; }

        public string ImageFileExtension { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductForQuerySearchViewModel>()
                .ForMember(dest => dest.ImageUrlPath, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.UrlPath : (src.Images.Any() ? src.Images.FirstOrDefault().UrlPath : "")))
                .ForMember(dest => dest.ImageFileExtension, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.FileExtension : (src.Images.Any() ? src.Images.FirstOrDefault().FileExtension : "")));

            configuration.CreateMap<ProductCacheViewModel, ProductForQuerySearchViewModel>()
                .ForMember(dest => dest.ImageUrlPath, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.UrlPath : (src.Images.Any() ? src.Images.FirstOrDefault().UrlPath : "")))
                .ForMember(dest => dest.ImageFileExtension, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.FileExtension : (src.Images.Any() ? src.Images.FirstOrDefault().FileExtension : "")));
        }
    }
}
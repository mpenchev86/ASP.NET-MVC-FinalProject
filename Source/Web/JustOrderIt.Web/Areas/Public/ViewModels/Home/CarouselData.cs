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

    public class CarouselData : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public string EncodedId
        {
            get { return IdentifierProvider.EncodeIntIdStatic(this.Id); }
        }

        public string ImageUrlPath { get; set; }

        public string ImageFileExtension { get; set; }

        public string Title { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, CarouselData>()
                .ForMember(dest => dest.ImageUrlPath, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.UrlPath : string.Empty))
                .ForMember(dest => dest.ImageFileExtension, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.FileExtension : string.Empty))
                ;
        }
    }
}
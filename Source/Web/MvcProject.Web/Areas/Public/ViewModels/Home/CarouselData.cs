namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CarouselData : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public string Image { get; set; }

        public string Title { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, CarouselData>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Image, opt => opt
                    .MapFrom(src => 
                        src.MainImage.UrlPath + 
                        src.MainImage.OriginalFileName + 
                        src.MainImage.FileExtension))
                ;
        }
    }
}
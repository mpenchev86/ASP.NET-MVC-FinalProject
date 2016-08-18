namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Images;
    using Infrastructure.Mapping;

    public class ProductSneakPeekViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public ImageForProductSneakPeekViewModel MainImage { get; set; }

        public string SellerName { get; set; }

        public decimal Price { get; set; }

        [UIHint("Rating")]
        public double? Rating { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductSneakPeekViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.UserName))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.AllTimeAverageRating))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
                ;
        }
    }
}
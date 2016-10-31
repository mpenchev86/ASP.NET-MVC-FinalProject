namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Descriptions;
    using Images;
    using Infrastructure.Mapping;

    public class ProductOfCategoryViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public decimal UnitPrice { get; set; }

        public int? MainImageId { get; set; }

        public ImageForThumbnailProductViewModel MainImage { get; set; }

        public int? DescriptionId { get; set; }

        public DescriptionForProductOfCategoryViewModel Description { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductFullViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
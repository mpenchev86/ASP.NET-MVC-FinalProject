﻿namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Data.Models.Catalog;
    using Images;
    using Infrastructure.Mapping;

    public class ProductWithTagViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public decimal UnitPrice { get; set; }

        public ImageForThumbnailProductViewModel MainImage { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductWithTagViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
﻿namespace JustOrderIt.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Common.GlobalConstants;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;
    using Services.Web;

    public class ProductForUserProfileOrder: BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public string EncodedId
        {
            get { return IdentifierProvider.EncodeIntIdStatic(this.Id); }
        }

        public string Title { get; set; }

        public decimal UnitPrice { get; set; }

        public int QuantityInStock { get; set; }

        public decimal? ShippingPrice { get; set; }

        public string ImageUrlPath { get; set; }

        public string ImageFileExtension { get; set; }

        public int Rating { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductForUserProfileOrder>()
                .ForMember(dest => dest.ImageUrlPath, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.UrlPath : (src.Images.Any() ? src.Images.FirstOrDefault().UrlPath : "")))
                .ForMember(dest => dest.ImageFileExtension, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.FileExtension : (src.Images.Any() ? src.Images.FirstOrDefault().FileExtension : "")));
        }
    }
}
namespace JustOrderIt.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Common.GlobalConstants;
    using Data.Models;
    using Data.Models.Catalog;
    using Descriptions;
    using Images;
    using Infrastructure.Mapping;
    using Services.Web;

    public class ProductForCategorySearchViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IMapFrom<ProductCacheViewModel>, IHaveCustomMappings
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

        public decimal UnitPrice { get; set; }

        public string ImageUrlPath { get; set; }

        public string ImageFileExtension { get; set; }

        public int? DescriptionId { get; set; }

        [UIHint("Rating")]
        [Range(ValidationConstants.ProductAllTimeAverageRatingMin, ValidationConstants.ProductAllTimeAverageRatingMax)]
        public double? AllTimeAverageRating { get; set; }

        [UIHint("PostBackDescriptionForCategory")]
        public DescriptionForCategorySearchViewModel Description { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductForCategorySearchViewModel>()
                .ForMember(dest => dest.ImageUrlPath, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.UrlPath : (src.Images.Any() ? src.Images.FirstOrDefault().UrlPath : "")))
                .ForMember(dest => dest.ImageFileExtension, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.FileExtension : (src.Images.Any() ? src.Images.FirstOrDefault().FileExtension : "")))
                .ForMember(dest => dest.AllTimeAverageRating, opt => opt.MapFrom(
                            src => src.Votes.Any() ? (double?)src.Votes.Average(v => v.VoteValue) : null));

            configuration.CreateMap<ProductCacheViewModel, ProductForCategorySearchViewModel>()
                .ForMember(dest => dest.ImageUrlPath, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.UrlPath : (src.Images.Any() ? src.Images.FirstOrDefault().UrlPath : "")))
                .ForMember(dest => dest.ImageFileExtension, opt => opt.MapFrom(
                            src => src.MainImage != null ? src.MainImage.FileExtension : (src.Images.Any() ? src.Images.FirstOrDefault().FileExtension : "")))
                .ForMember(dest => dest.AllTimeAverageRating, opt => opt.MapFrom(
                            src => src.Votes.Any() ? (double?)src.Votes.Average(v => v.VoteValue) : null));
        }
    }
}
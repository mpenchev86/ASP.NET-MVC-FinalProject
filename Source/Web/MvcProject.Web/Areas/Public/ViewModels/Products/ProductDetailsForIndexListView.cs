namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using MvcProject.Common.GlobalConstants;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using ViewModels;
    using Services.Web;
    using Images;

    public class ProductDetailsForIndexListView : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public string EncodedId
        {
            get { return IdentifierProvider.EncodeIntIdStatic(this.Id); }
        }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }
        
        [Required]
        [Range(typeof(decimal), ValidationConstants.ProductUnitPriceMinString, ValidationConstants.ProductUnitPriceMaxString)]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        //public int? MainImageId { get; set; }

        //public virtual ImageForThumbnailProductViewModel MainImage { get; set; }

        /// <summary>
        /// Gets or sets the product's Main Image url path. If none is found, the first found image is used.
        /// </summary>
        public string ImageUrlPath { get; set; }

        /// <summary>
        /// Gets or sets the product's Main Image (or the first image found) file extension.
        /// </summary>
        public string ImageFileExtension { get; set; }

        [LongDateTimeFormat]
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductDetailsForIndexListView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.AllTimeAverageRating, opt => opt.MapFrom(
                //            src => src.Votes.Any() ? src.Votes.Average(v => v.VoteValue) : default(double?)))
                //.ForMember(dest => dest.MainImageId, opt => opt.MapFrom(src => src.MainImageId))
                //.ForMember(dest => dest.MainImage, opt => opt.MapFrom(
                //            src => new ImageForThumbnailProductViewModel
                //            {
                //                OriginalFileName = src.MainImage != null ? src.MainImage.OriginalFileName : string.Empty,
                //                FileExtension = src.MainImage != null ? src.MainImage.FileExtension : string.Empty,
                //                UrlPath = src.MainImage != null ? src.MainImage.UrlPath : string.Empty
                //            }))
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
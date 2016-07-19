namespace MvcProject.Web.Areas.Public.ViewModels.Home
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
    public class ProductDetailsForIndexListView : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        private IIdentifierProvider identifierProvider;

        public ProductDetailsForIndexListView()
        {
            this.identifierProvider = new IdentifierProvider();
        }

        public string EncodedId
        {
            get { return this.identifierProvider.EncodeIntId(this.Id); }
        }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }

        [Range(ValidationConstants.ProductAllTimeItemsSoldMin, ValidationConstants.ProductAllTimeItemsSoldMax)]
        public int AllTimeItemsSold { get; set; }

        [Range(ValidationConstants.ProductAllTimeAverageRatingMin, ValidationConstants.ProductAllTimeAverageRatingMax)]
        public double? AllTimeAverageRating { get; set; }

        [Required]
        [Range(typeof(decimal), ValidationConstants.ProductUnitPriceMinString, ValidationConstants.ProductUnitPriceMaxString)]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [LongDateTimeFormat]
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductDetailsForIndexListView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
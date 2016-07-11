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
    using ViewModel;

    public class ProductDetailsForIndexListView : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }

        [Range(ValidationConstants.ProductAllTimeItemsSoldMin, ValidationConstants.ProductAllTimeItemsSoldMax)]
        public int AllTimeItemsSold { get; set; }

        [Range(ValidationConstants.ProductAllTimeAverageRatingMin, ValidationConstants.ProductAllTimeAverageRatingMax)]
        public int AllTimeAverageRating { get; set; }

        [LongDateTimeFormat]
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductDetailsForIndexListView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
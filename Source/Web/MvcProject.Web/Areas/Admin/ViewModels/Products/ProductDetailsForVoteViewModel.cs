﻿namespace MvcProject.Web.Areas.Admin.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using MvcProject.GlobalConstants;

    public class ProductDetailsForVoteViewModel : BaseAdminViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductDetailsForVoteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
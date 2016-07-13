﻿namespace MvcProject.Web.Areas.Common.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Mapping;
    using Infrastructure.Sanitizer;
    using Services.Web;

    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        private ISanitizer sanitizer;

        public ProductViewModel()
        {
            this.sanitizer = new HtmlSanitizerAdapter();
        }

        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Home.Index), ErrorMessageResourceName = nameof(Resources.Home.Index.RequiredField))]
        public string Title { get; set; }

        [UIHint(GlobalConstants.Templates.CustomStringTemplate)]
        public string Description { get; set; }

        public string SanitizedDescription => this.sanitizer.Sanitize(this.Description);

        public string Category { get; set; }

        public decimal UnitPrice { get; set; }

        public string Url
        {
            get
            {
                return string.Format("/Product/{0}", this.Id);
            }
        }

        public Image MainImage { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ShortDescription));
        }
    }
}
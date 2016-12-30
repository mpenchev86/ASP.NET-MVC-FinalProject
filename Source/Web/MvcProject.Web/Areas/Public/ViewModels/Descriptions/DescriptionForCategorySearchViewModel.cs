﻿namespace MvcProject.Web.Areas.Public.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;
    using Properties;

    public class DescriptionForCategorySearchViewModel : BasePublicViewModel<int>, IMapFrom<Description>, IMapFrom<DescriptionCacheViewModel>, IHaveCustomMappings
    {
        private ICollection<PropertyForCategorySearchViewModel> properties;

        public DescriptionForCategorySearchViewModel()
        {
            this.properties = new /*HashSet*/List<PropertyForCategorySearchViewModel>();
        }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [UIHint("PostBackPropertiesForCategory")]
        public ICollection<PropertyForCategorySearchViewModel> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Description, DescriptionForCategorySearchViewModel>()
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(
                            src => src.Properties.Select(p => new PropertyForCategorySearchViewModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Value = p.Value
                            })));

            configuration.CreateMap<DescriptionCacheViewModel, DescriptionForCategorySearchViewModel>()
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(
                            src => src.Properties.Select(p => new PropertyForCategorySearchViewModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Value = p.Value
                            })));
        }
    }
}
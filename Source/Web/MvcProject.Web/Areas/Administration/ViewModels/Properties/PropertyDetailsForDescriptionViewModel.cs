namespace MvcProject.Web.Areas.Administration.ViewModels.Properties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PropertyDetailsForDescriptionViewModel : BaseAdminViewModel<int>, IMapFrom<Property>, IHaveCustomMappings
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Value { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Property, PropertyDetailsForDescriptionViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
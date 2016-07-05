namespace MvcProject.Web.Areas.Admin.ViewModels.Properties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Descriptions;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class PropertyViewModel : BaseAdminViewModel<int>, IMapFrom<Property>, IHaveCustomMappings
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Value { get; set; }

        [UIHint("DropDown")]
        public int DescriptionId { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Property, PropertyViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DescriptionId, opt => opt.MapFrom(src => src.DescriptionId));
        }
    }
}
namespace MvcProject.Web.Areas.Public.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Data.Models;
    using Properties;
    using Infrastructure.Mapping;
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;

    public class DescriptionForProductFullViewModel : IMapFrom<Description>, IHaveCustomMappings
    {
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public ICollection<PropertyForProductFullViewModel> Properties { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Description, DescriptionForProductFullViewModel>()
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(
                            src => src.Properties.Select(p => new PropertyForProductFullViewModel
                            {
                                Name = p.Name,
                                Value = p.Value
                            })));
        }
    }
}
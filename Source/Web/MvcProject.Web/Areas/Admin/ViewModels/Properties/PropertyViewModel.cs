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
    using Infrastructure.Mapping;

    public class PropertyViewModel : /*BaseAdminViewModel, */IMapFrom<Property>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Value { get; set; }

        //[UIHint("DescriptionDropDown")]
        public int DescriptionId { get; set; }

        public DescriptionDetailsForPropertyViewModel Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Property, PropertyViewModel>()
                .ForMember(dest => dest.DescriptionId, opt => opt.MapFrom(src => src.DescriptionId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(
                           src => new DescriptionDetailsForPropertyViewModel
                           {
                               Id = src.Description.Id,
                               Content = src.Description.Content,
                               CreatedOn = src.Description.CreatedOn,
                               ModifiedOn = src.Description.ModifiedOn
                           }));
        }
    }
}
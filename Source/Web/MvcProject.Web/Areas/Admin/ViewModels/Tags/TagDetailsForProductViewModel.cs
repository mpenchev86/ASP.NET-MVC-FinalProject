namespace MvcProject.Web.Areas.Admin.ViewModels.Tags
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Data.Models.EntityContracts;
    using Infrastructure.Mapping;
    using MvcProject.GlobalConstants;
    using Products;

    public class TagDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Tag>, IHaveCustomMappings
    {
        [Required]
        [MaxLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Tag, TagDetailsForProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                ;
        }
    }
}
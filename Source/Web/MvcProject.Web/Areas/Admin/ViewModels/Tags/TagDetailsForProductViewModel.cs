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
        // Without it, Products\Read gives an error:
        // The type 'MvcProject.Web.Areas.Admin.ViewModels.Tags.TagDetailsForProductViewModel' appears in two
        // structurally incompatible initializations within a single LINQ to Entities query. A type can be
        // initialized in two places in the same query, but only if the same properties are set in both places
        // and those properties are set in the same order.
        //[Key]
        public int Id { get; set; }

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
namespace MvcProject.Web.Areas.Administration.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class DescriptionDetailsForPropertyViewModel : BaseAdminViewModel<int>, IMapFrom<Description>, IHaveCustomMappings
    {
        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Description, DescriptionDetailsForPropertyViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
namespace MvcProject.Web.Areas.Public.ViewModel.Votes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Mapping;

    public class VoteDetailsForIndexListViewProduct : BasePublicViewModel<int>, IMapFrom<Vote>, IHaveCustomMappings
    {
        [Required]
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int VoteValue { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Vote, VoteDetailsForIndexListViewProduct>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
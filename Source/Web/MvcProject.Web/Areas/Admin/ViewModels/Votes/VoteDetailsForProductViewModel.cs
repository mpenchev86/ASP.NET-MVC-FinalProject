namespace MvcProject.Web.Areas.Admin.ViewModels.Votes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;

    public class VoteDetailsForProductViewModel : IMapFrom<Vote>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int VoteValue { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Vote, VoteDetailsForProductViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                ;
        }
    }
}
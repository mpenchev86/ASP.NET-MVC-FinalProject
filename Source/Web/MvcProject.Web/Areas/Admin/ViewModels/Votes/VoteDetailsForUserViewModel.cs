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

    public class VoteDetailsForUserViewModel : BaseAdminViewModel, IMapFrom<Vote>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int VoteValue { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Vote, VoteDetailsForUserViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                ;
        }
    }
}
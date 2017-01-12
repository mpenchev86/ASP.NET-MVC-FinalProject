namespace MvcProject.Web.Areas.Public.ViewModels.Votes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class VoteForProductFullViewModel : IMapFrom<Vote>, IHaveCustomMappings
    {
        public int VoteValue { get; set; }
        
        public int ProductId { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Vote, VoteForProductFullViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                ;
        }
    }
}
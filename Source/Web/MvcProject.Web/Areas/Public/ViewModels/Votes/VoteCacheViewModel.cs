namespace MvcProject.Web.Areas.Public.ViewModels.Votes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class VoteCacheViewModel : BasePublicViewModel<int>, IMapFrom<Vote>/*, IHaveCustomMappings*/
    {
        public int VoteValue { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; }

        //public string UserName { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    configuration.CreateMap<Vote, VoteCacheViewModel>()
        //        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        //}
    }
}
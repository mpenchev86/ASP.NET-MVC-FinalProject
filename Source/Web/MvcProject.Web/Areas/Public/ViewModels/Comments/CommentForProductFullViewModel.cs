namespace MvcProject.Web.Areas.Public.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CommentForProductFullViewModel : IMapFrom<Comment>, IMapFrom<Vote>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UserVote { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentForProductFullViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            configuration.CreateMap<Vote, CommentForProductFullViewModel>()
                .ForMember(dest => dest.UserVote, opt => opt.MapFrom(src => src.VoteValue));
        }
    }
}
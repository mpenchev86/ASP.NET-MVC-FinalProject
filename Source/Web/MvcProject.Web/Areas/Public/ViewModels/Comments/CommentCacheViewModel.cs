namespace MvcProject.Web.Areas.Public.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;

    public class CommentCacheViewModel : BasePublicViewModel<int>, IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int ProductId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentCacheViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
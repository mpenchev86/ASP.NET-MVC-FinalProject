namespace MvcProject.Web.Areas.Public.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserProfileViewModel : BasePublicViewModel<string>, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        //public string Email { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserProfileViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
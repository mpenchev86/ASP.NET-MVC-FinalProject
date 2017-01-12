namespace JustOrderIt.Web.Areas.Administration.ViewModels.Users
{
    using System;
    using AutoMapper;
    using Data.Models.Identity;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Web.Infrastructure.Mapping;

    public class UserDetailsForProductViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserDetailsForProductViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
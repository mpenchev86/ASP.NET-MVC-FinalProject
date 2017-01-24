namespace JustOrderIt.Web.Areas.Administration.ViewModels.Roles
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Identity;
    using Infrastructure.Mapping;

    public class RoleDetailsForUserViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationRole>, IMapFrom<ApplicationUserRole>, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ApplicationUserRole, RoleDetailsForUserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));
        }
    }
}
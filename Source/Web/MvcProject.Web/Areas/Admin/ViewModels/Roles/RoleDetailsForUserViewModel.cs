namespace MvcProject.Web.Areas.Admin.ViewModels.Roles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RoleDetailsForUserViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationRole>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationRole, RoleDetailsForUserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                ;
        }
    }
}
namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class IndexUserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Role { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, IndexUserViewModel>()
                .ForMember(vm => vm.Name, opt => opt.MapFrom(m => m.UserName))
                .ForMember(vm => vm.Role, opt => opt.MapFrom(m => m.Roles.FirstOrDefault()));
        }
    }
}
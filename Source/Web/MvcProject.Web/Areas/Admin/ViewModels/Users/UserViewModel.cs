namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    using AutoMapper;
    using Data.DbAccessConfig;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data;

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string MainRole { get; set; }

        public IList<string> Roles
        {
            get
            {
                var userManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var list = userManager.GetRoles(this.Id);
                return list;
            }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
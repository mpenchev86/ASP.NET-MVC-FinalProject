namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    using AutoMapper;
    using Data.DbAccessConfig;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data;

    public class UserViewModel : BaseAdminViewModel, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the user as displayed in the user interface,
        /// depending on whether the user has been deleted from the system or not.
        /// </summary>
        public string DisplayName { get; set; }

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

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(
                           src => src.IsDeleted ? GlobalConstants.ApplicationSpecialStrings.UserNameDeletedUser : src.UserName))
                ;
        }
    }
}
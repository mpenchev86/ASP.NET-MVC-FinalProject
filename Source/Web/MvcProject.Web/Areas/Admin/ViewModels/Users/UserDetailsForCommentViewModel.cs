namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    public class UserDetailsForCommentViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        //[Key]
        //public string Id { get; set; }

        //public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the user as displayed in the user interface,
        /// depending on whether the user has been deleted from the system or not.
        /// </summary>
        public string UserName { get; set; }

        //public string MainRole { get; set; }

        //public IList<string> Roles
        //{
        //    get
        //    {
        //        var userManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //        var list = userManager.GetRoles(this.UserId);
        //        return list;
        //    }
        //}

        //[LongDateTimeFormat]
        //public DateTime CreatedOn { get; set; }

        //[LongDateTimeFormat]
        //public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                ;
        }
    }
}
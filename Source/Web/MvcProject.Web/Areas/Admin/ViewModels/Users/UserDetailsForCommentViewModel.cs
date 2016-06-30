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
    using Roles;

    public class UserDetailsForCommentViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        //private ICollection<RoleDetailsForUserViewModel> roles;

        //public UserDetailsForCommentViewModel()
        //{
        //    this.roles = new HashSet<RoleDetailsForUserViewModel>();
        //}

        /// <summary>
        /// Gets or sets the name of the user as displayed in the user interface.
        /// </summary>
        /// <value>
        /// The name of the user as displayed in the user interface.
        /// </value>
        public string UserName { get; set; }

        //public ICollection<RoleDetailsForUserViewModel> Roles
        //{
        //    get { return this.roles; }
        //    set { this.roles = value; }
        //}

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserDetailsForCommentViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                //.ForMember(dest => dest.Roles, opt => opt.MapFrom(
                //            src => src.Roles.Select(r => new RoleDetailsForUserViewModel
                //            {
                //                Id = r.RoleId,
                //                Name = r.RoleName
                //            })))
                ;
        }
    }
}
﻿namespace MvcProject.Web.Areas.Administration.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserDetailsForRoleViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        /// <summary>
        /// Gets or sets the name of the user as displayed in the user interface.
        /// </summary>
        /// <value>
        /// The name of the user as displayed in the user interface.
        /// </value>
        [Required]
        public string UserName { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserDetailsForRoleViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
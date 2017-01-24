namespace JustOrderIt.Web.Areas.Administration.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Identity;
    using Infrastructure.Mapping;

    public class UserDetailsForRoleViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>, IMapFrom<ApplicationUserRole>, IHaveCustomMappings
    {
        /// <summary>
        /// Gets or sets the name of the user as displayed in the user interface.
        /// </summary>
        /// <value>
        /// The name of the user as displayed in the user interface.
        /// </value>
        [Required]
        public string UserName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ApplicationUserRole, UserDetailsForRoleViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
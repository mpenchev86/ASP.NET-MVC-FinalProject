namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserDetailsForVoteViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        [Key]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user as displayed in the user interface,
        /// depending on whether the user has been deleted from the system or not.
        /// </summary>
        public string DisplayName { get; set; }

        //[LongDateTimeFormat]
        //public DateTime CreatedOn { get; set; }

        //[LongDateTimeFormat]
        //public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(
                           src => src.IsDeleted ? GlobalConstants.ApplicationSpecialStrings.UserNameDeletedUser : src.UserName))
                ;
        }
    }
}
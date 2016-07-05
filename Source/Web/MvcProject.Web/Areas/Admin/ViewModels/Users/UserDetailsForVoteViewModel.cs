namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserDetailsForVoteViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        /// <summary>
        /// Gets or sets the name of the user as displayed in the user interface.
        /// </summary>
        /// <value>
        /// The name of the user as displayed in the user interface.
        /// </value>
        public string UserName { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserDetailsForVoteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
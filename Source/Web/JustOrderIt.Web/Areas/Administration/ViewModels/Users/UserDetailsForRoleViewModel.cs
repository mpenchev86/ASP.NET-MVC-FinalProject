namespace JustOrderIt.Web.Areas.Administration.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Data.Models.Identity;
    using Infrastructure.Mapping;

    public class UserDetailsForRoleViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>
    {
        /// <summary>
        /// Gets or sets the name of the user as displayed in the user interface.
        /// </summary>
        /// <value>
        /// The name of the user as displayed in the user interface.
        /// </value>
        [Required]
        public string UserName { get; set; }
    }
}
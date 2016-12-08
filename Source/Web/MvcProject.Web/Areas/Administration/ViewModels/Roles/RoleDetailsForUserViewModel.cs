namespace MvcProject.Web.Areas.Administration.ViewModels.Roles
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

    public class RoleDetailsForUserViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationRole>
    {
        [Required]
        public string Name { get; set; }
    }
}
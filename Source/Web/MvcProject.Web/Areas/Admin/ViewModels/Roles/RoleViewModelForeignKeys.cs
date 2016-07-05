namespace MvcProject.Web.Areas.Admin.ViewModels.Roles
{
    using System.Collections.Generic;
    using MvcProject.Web.Areas.Admin.ViewModels.Users;

    public class RoleViewModelForeignKeys
    {
        public IEnumerable<UserDetailsForRoleViewModel> Users { get; set; }
    }
}
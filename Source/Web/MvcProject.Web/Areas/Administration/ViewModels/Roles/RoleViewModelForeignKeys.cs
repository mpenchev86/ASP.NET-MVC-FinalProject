namespace MvcProject.Web.Areas.Administration.ViewModels.Roles
{
    using System.Collections.Generic;
    using MvcProject.Web.Areas.Administration.ViewModels.Users;

    public class RoleViewModelForeignKeys
    {
        public IEnumerable<UserDetailsForRoleViewModel> Users { get; set; }
    }
}
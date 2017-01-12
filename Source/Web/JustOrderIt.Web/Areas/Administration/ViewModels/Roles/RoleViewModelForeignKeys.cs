namespace JustOrderIt.Web.Areas.Administration.ViewModels.Roles
{
    using System.Collections.Generic;
    using JustOrderIt.Web.Areas.Administration.ViewModels.Users;

    public class RoleViewModelForeignKeys
    {
        public IEnumerable<UserDetailsForRoleViewModel> Users { get; set; }
    }
}
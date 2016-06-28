namespace MvcProject.Web.Areas.Admin.ViewModels.Roles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using MvcProject.Web.Areas.Admin.ViewModels.Users;

    public class RoleViewModelForeignKeys
    {
        public IEnumerable<UserDetailsForRoleViewModel> Users { get; set; }
    }
}
namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using System.Collections.Generic;
    using Roles;

    public class UserViewModelForeignKeys
    {
        public IEnumerable<RoleDetailsForUserViewModel> Roles { get; set; }
    }
}
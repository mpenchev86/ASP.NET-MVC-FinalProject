namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Roles;

    public class UserViewModelForeignKeys
    {
        public IEnumerable<RoleDetailsForUserViewModel> Roles { get; set; }
    }
}
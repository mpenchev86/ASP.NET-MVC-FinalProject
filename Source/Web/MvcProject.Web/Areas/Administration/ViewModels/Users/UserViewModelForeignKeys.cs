namespace MvcProject.Web.Areas.Administration.ViewModels.Users
{
    using System.Collections.Generic;
    using Roles;

    public class UserViewModelForeignKeys
    {
        /// <summary>
        /// Gets or sets all application roles projected to RoleDetailsForUserViewModel objects
        /// and transfered to the Kendo MultiSelect element in the view exposing the Roles
        /// navigation property of ApplicationUser
        /// </summary>
        /// <value>
        /// All application roles projected to RoleDetailsForUserViewModel objects
        /// and transfered to the Kendo MultiSelect element in the view exposing the Roles
        /// navigation property of ApplicationUser
        /// </value>
        public IEnumerable<RoleDetailsForUserViewModel> Roles { get; set; }
    }
}
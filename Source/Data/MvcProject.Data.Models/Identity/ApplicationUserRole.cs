namespace MvcProject.Data.Models.Identity
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Implements the IdentityUserRole class which represents the junction table of user's and role's entities.
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole, IBaseEntityModel<int>
    {
        public ApplicationUserRole()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the Id property inherited from IBaseEntityModel&lt;int&gt; but is not mapped because ApplicationUserRole
        /// represents the junction table of user's and role's entities.
        /// </summary>
        /// <value>
        /// The Id property inherited from IBaseEntityModel&lt;int&gt; but is not mapped because ApplicationUserRole
        /// represents the junction table of user's and role's entities.
        /// </value>
        [NotMapped]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user corresponding to the UserId foreign key.
        /// </summary>
        /// <value>
        /// The name of the user corresponding to the UserId foreign key.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the role corresponding to the RoleId foreign key.
        /// </summary>
        /// <value>
        /// The name of the role corresponding to the RoleId foreign key.
        /// </value>
        public string RoleName { get; set; }
    }
}

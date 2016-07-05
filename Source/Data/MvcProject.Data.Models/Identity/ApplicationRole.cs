namespace MvcProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Threading.Tasks;
    using EntityContracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Inherits the IdentityRole class which is the base concrete ASP.NET implementation of a role entity.
    /// </summary>
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>, IBaseEntityModel<string>, IDeletableEntity, IAuditInfo, IAdministerable
    {
        public ApplicationRole()
            : base()
        {
            // Initialized the new role with a Guid Id. The default implementation of IdentityRole takes care of it automatically.
            // The following error occurs otherwise:
            // [DbEntityValidationException: Entity Validation Failed - errors follow:
            // MvcProject.Data.Models.ApplicationRole failed validation
            // - Id : The Id field is required.
            this.Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        /// <value>
        /// The date and time when the entity was created.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        /// <value>
        /// The date and time when the entity was last modified.
        /// </value>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is marked as deleted
        /// </summary>
        /// <value>
        /// A value indicating whether the entity is marked as deleted
        /// </value>
        [Index]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was marked as deleted
        /// </summary>
        /// <value>
        /// The date and time when the entity was marked as deleted
        /// </value>
        public DateTime? DeletedOn { get; set; }

        public async Task<IdentityResult> GenerateRoleAsync(RoleManager<ApplicationRole> roleManager)
        {
            var identrole = await roleManager.CreateAsync(this);

            // Customize role here
            return identrole;
        }
    }
}

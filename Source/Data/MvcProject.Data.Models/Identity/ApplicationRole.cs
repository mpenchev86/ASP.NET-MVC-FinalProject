namespace MvcProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Threading.Tasks;
    using EntityContracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

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

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public async Task<IdentityResult> GenerateRoleAsync(RoleManager<ApplicationRole> roleManager)
        {
            var identrole = await roleManager.CreateAsync(this);

            // Customize role here
            return identrole;
        }
    }
}

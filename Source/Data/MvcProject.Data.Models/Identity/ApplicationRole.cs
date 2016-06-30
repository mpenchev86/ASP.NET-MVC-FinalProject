namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EntityContracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>, IBaseEntityModel<string>, IDeletableEntity, IAuditInfo, IAdministerable
    {
        //private ICollection<ApplicationUser> applicationUsers;

        public ApplicationRole()
            : base()
        {
            // Initialized the new role with a Guid Id. The default implementation of IdentityRole takes care of it automatically.
            // The following error occurs otherwise:
            // [DbEntityValidationException: Entity Validation Failed - errors follow:
            // MvcProject.Data.Models.ApplicationRole failed validation
            // - Id : The Id field is required.
            this.Id = Guid.NewGuid().ToString();
            //this.applicationUsers = new HashSet<ApplicationUser>();
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

        //public virtual ICollection<ApplicationUser> ApplicationUsers
        //{
        //    get { return this.applicationUsers; }
        //    set { this.applicationUsers = value; }
        //}

        public async Task<IdentityResult> GenerateRoleAsync(RoleManager<ApplicationRole/*, string*/> roleManager)
        {
            var identrole = await roleManager.CreateAsync(this);

            // Customize role here
            return identrole;
        }
    }
}

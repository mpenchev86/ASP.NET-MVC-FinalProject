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

    public class ApplicationRole : IdentityRole, IBaseEntityModel<string>, IAdministerable
    {
        //private ICollection<ApplicationUser> applicationUsers;

        public ApplicationRole()
        {
            //this.applicationUsers = new HashSet<ApplicationUser>();
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

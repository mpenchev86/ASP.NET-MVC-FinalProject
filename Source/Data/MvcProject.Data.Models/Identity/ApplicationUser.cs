namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using EntityContracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<string, IdentityUserLogin, ApplicationUserRole, IdentityUserClaim>, IBaseEntityModel<string>, IAdministerable
    {
        private ICollection<Comment> comments;
        //private ICollection<ApplicationRole> applicationRoles;
        private ICollection<Vote> votes;

        public ApplicationUser()
            : base()
        {
            // Initialized the new role with a Guid Id. The default implementation of IdentityRole takes care of it automatically.
            // The following error occurs otherwise:
            // [DbEntityValidationException: Entity Validation Failed - errors follow:
            // MvcProject.Data.Models.ApplicationRole failed validation
            // - Id : The Id field is required.
            this.Id = Guid.NewGuid().ToString();

            // Prevents the following datetime2 convertion exception:
            // The conversion of a datetime2 data type to a datetime data type resulted in an out-of-range value.
            // The statement has been terminated.
            this.CreatedOn = DateTime.Now;
            this.comments = new HashSet<Comment>();
            //this.applicationRoles = new HashSet<ApplicationRole>();
            this.votes = new HashSet<Vote>();
        }

        //public virtual ICollection<ApplicationRole> ApplicationRoles
        //{
        //    get { return this.applicationRoles; }
        //    set { this.applicationRoles = value; }
        //}

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}

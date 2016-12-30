namespace MvcProject.Data.Models.Identity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Catalog;
    using Contracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Orders;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<string, IdentityUserLogin, ApplicationUserRole, IdentityUserClaim>, IBaseEntityModel<string>, IDeletableEntity, IAuditInfo, IAdministerable
    {
        private ICollection<Comment> comments;
        private ICollection<Vote> votes;
        private ICollection<Order> orders;

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
            this.votes = new HashSet<Vote>();
            this.orders = new HashSet<Order>();
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

        /// <summary>
        /// Gets or sets the collection of comments submitted by the user.
        /// </summary>
        /// <value>
        /// The collection of comments submitted by the user.
        /// </value>
        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        /// <summary>
        /// Gets or sets the collection of votes submitted by the user.
        /// </summary>
        /// <value>
        /// The collection of votes submitted by the user.
        /// </value>
        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        /// <summary>
        /// Gets or sets the collection of orders of the user.
        /// </summary>
        /// <value>
        /// The collection of orders of the user.
        /// </value>
        public virtual ICollection<Order> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
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

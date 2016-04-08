namespace MvcProject.Data.DbAccessConfig
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Constants;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;
    using Models.EntityContracts;

    public class MvcProjectDbContext : IdentityDbContext<ApplicationUser>, IMvcProjectDbContext
    {
        public MvcProjectDbContext()
            : base(Common.Constants.DbAccess.DefaultConnectionString, throwIfV1Schema: false)
        {
        }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Description> Descriptions { get; set; }

        public IDbSet<Image> Images { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Property> Properties { get; set; }

        //public IDbSet<ShippingInfo> ShippingInfoes { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public static MvcProjectDbContext Create()
        {
            return new MvcProjectDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            //try
            //{
            //    return base.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    // Retrieve the error messages as a list of strings.
            //    var errorMessages = ex.EntityValidationErrors
            //            .SelectMany(x => x.ValidationErrors)
            //            .Select(x => x.ErrorMessage);

            //    // Join the list to a single string.
            //    var fullErrorMessage = string.Join("; ", errorMessages);

            //    // Combine the original exception message with the new one.
            //    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

            //    // Throw a new DbEntityValidationException with the improved exception message.
            //    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            //}

            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}

namespace MvcProject.Data.DbAccessConfig.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;
    using Models.Contracts;
    using MvcProject.GlobalConstants;

    public class MvcProjectDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserLogin, ApplicationUserRole, IdentityUserClaim>, IMvcProjectDbContext
    {
        public MvcProjectDbContext()
            : base(DbAccess.DefaultConnectionString/*, throwIfV1Schema: false*/)
        {
        }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Description> Descriptions { get; set; }

        public IDbSet<Image> Images { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Property> Properties { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public IDbSet<Statistics> Statistics { get; set; }

        public static MvcProjectDbContext Create()
        {
            return new MvcProjectDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAdditionalInfoRules();

            // Debug db operations.
            // Source: http://stackoverflow.com/a/10676526/4491770
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                // Add the original exception as the innerException
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex);
            }

            // return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Statistics>()
                .HasEntitySetName("Statistics");

            modelBuilder.Entity<Product>()
                .HasMany<Tag>(p => p.Tags)
                .WithMany(t => t.Products)
                .Map(pt =>
                {
                    pt.MapLeftKey("ProductId");
                    pt.MapRightKey("TagId");
                    pt.ToTable("ProductsTags");
                });

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAdditionalInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(e =>
                        (e.Entity is IAuditInfo || e.Entity is IDeletableEntity) &&
                        ((e.State == EntityState.Added) ||
                        (e.State == EntityState.Modified))))
            {
                if (entry.Entity is IAuditInfo)
                {
                    this.ApplyAuditInfoRules(entry);
                }

                if (entry.Entity is IDeletableEntity)
                {
                    this.ApplyDeletableRules(entry);
                }
            }
        }

        private void ApplyAuditInfoRules(DbEntityEntry entry)
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

        private void ApplyDeletableRules(DbEntityEntry entry)
        {
            var entity = (IDeletableEntity)entry.Entity;
            if (entity.IsDeleted == true)
            {
                if (entity.DeletedOn == null)
                {
                    entity.DeletedOn = DateTime.Now;
                }
            }
            else
            {
                entity.DeletedOn = null;
            }
        }
    }
}

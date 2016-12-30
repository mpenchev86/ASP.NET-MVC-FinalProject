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
    using Models.Catalog;
    using Models.Contracts;
    using Models.Identity;
    using Models.Media;
    using Models.Orders;
    using Models.Search;
    using MvcProject.Common.GlobalConstants;

    public class MvcProjectDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserLogin, ApplicationUserRole, IdentityUserClaim>, IMvcProjectDbContext
    {
        public MvcProjectDbContext()
            : base(DbAccess.ApplicationConnectionStringName/*, throwIfV1Schema: false*/)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<SearchFilter> SearchFilters { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Description> Descriptions { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Keyword> Keywords { get; set; }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Property> Properties { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<Vote> Votes { get; set; }

        public virtual IDbSet<ApplicationUserRole> UserRoles { get; set; }

        public virtual IDbSet<Order> Orders { get; set; }

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
            modelBuilder.Entity<Product>()
                .HasMany<Tag>(p => p.Tags)
                .WithMany(t => t.Products)
                .Map(pt =>
                {
                    pt.MapLeftKey("ProductId");
                    pt.MapRightKey("TagId");
                    pt.ToTable("ProductsTags");
                });

            modelBuilder.Entity<Category>()
                .HasMany<Keyword>(c => c.Keywords)
                .WithMany(k => k.Categories)
                .Map(pt =>
                {
                    pt.MapLeftKey("CategoryId");
                    pt.MapRightKey("KeywordId");
                    pt.ToTable("CategoriesKeywords");
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
                        (e.State == EntityState.Added || e.State == EntityState.Modified)))
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

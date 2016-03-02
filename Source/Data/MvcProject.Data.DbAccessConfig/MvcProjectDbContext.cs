namespace MvcProject.Data.DbAccessConfig
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GlobalConstants;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;
    using Models.EntityContracts;

    public class MvcProjectDbContext : IdentityDbContext<ApplicationUser>, IMvcProjectDbContext
    {
        public MvcProjectDbContext()
            : base(GlobalConstants.DbAccess.DefaultConnectionString, throwIfV1Schema: false)
        {
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Category> Category { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public static MvcProjectDbContext Create()
        {
            return new MvcProjectDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
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

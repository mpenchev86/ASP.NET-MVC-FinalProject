namespace MvcProject.Data.DbAccessConfig.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GlobalConstants;
    using Models;
    public sealed class Configuration : DbMigrationsConfiguration<MvcProjectDbContext>
    {
        public Configuration()
        {
            // TODO: Remove in production
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;

            this.ContextKey = GlobalConstants.DbAccess.DbMigrationsConfigurationContextKey;
        }

        protected override void Seed(MvcProjectDbContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method
            // to avoid creating duplicate seed data.E.g.

            // context.People.AddOrUpdate(
            //    p => p.FullName,
            //    new Person { FullName = "Andrew Peters" },
            //    new Person { FullName = "Brice Lambson" },
            //    new Person { FullName = "Rowan Miller" }
            //  );
            if (!context.Tags.Any())
            {
                context.Tags.AddOrUpdate(
                    t => t.Name,
                    new Tag { Name = "duvka" },
                    new Tag { Name = "vafla" },
                    new Tag { Name = "bonbon" });

                context.SaveChanges();
            }

            if (!context.ProductCategory.Any())
            {
                context.ProductCategory.AddOrUpdate(
                    c => c.Name,
                    new ProductCategory { Name = "duvki" },
                    new ProductCategory { Name = "vafli" },
                    new ProductCategory { Name = "bonbonki" });

                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.AddOrUpdate(
                    p => p.Name,
                    new Product { Name = "huba buba" },
                    new Product { Name = "vafla chudo" },
                    new Product { Name = "MnM" });

                context.SaveChanges();
            }
        }
    }
}

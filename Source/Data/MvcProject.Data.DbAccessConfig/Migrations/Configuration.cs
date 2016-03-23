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

            if (!context.Category.Any())
            {
                context.Category.AddOrUpdate(
                    c => c.Name,
                    new Category { Name = "duvki" },
                    new Category { Name = "vafli" },
                    new Category { Name = "bonbonki" });

                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.AddOrUpdate(
                    p => p.Title,
                    new Product
                    {
                        Title = "huba buba",
                        QuantityInStock = 3,
                        UnitPrice = 32.453M,
                        CategoryId = 1,
                        ShortDescription = "Gums massage your gums"
                    },
                    new Product
                    {
                        Title = "vafla chudo",
                        QuantityInStock = 14,
                        UnitPrice = 366662717.0002M,
                        CategoryId = 2,
                        ShortDescription = "Waffles you can't get enough of"
                    },
                    new Product
                    {
                        Title = "MnM",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        ShortDescription = "Eating the rainbow"
                    },
                    new Product
                    {
                        Title = "huba buba",
                        QuantityInStock = 3,
                        UnitPrice = 32.453M,
                        CategoryId = 1,
                        ShortDescription = "Gums massage your gums"
                    },
                    new Product
                    {
                        Title = "vafla chudo",
                        QuantityInStock = 14,
                        UnitPrice = 366662717.0002M,
                        CategoryId = 2,
                        ShortDescription = "Waffles you can't get enough of"
                    },
                    new Product
                    {
                        Title = "MnM",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        ShortDescription = "Eating the rainbow"
                    },
                    new Product
                    {
                        Title = "huba buba",
                        QuantityInStock = 3,
                        UnitPrice = 32.453M,
                        CategoryId = 1,
                        ShortDescription = "Gums massage your gums"
                    },
                    new Product
                    {
                        Title = "vafla chudo",
                        QuantityInStock = 14,
                        UnitPrice = 366662717.0002M,
                        CategoryId = 2,
                        ShortDescription = "Waffles you can't get enough of"
                    },
                    new Product
                    {
                        Title = "MnM",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        ShortDescription = "Eating the rainbow"
                    });

                context.SaveChanges();
            }
        }
    }
}

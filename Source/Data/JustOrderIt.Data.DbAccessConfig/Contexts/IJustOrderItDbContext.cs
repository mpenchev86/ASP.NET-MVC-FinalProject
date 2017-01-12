namespace JustOrderIt.Data.DbAccessConfig.Contexts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using JustOrderIt.Data.Models;
    using Models.Catalog;
    using Models.Identity;
    using Models.Media;
    using Models.Orders;
    using Models.Search;

    public interface IJustOrderItDbContext : IDisposable
    {
        IDbSet<Category> Categories { get; set; }

        IDbSet<SearchFilter> SearchFilters { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Description> Descriptions { get; set; }

        IDbSet<Image> Images { get; set; }

        IDbSet<Keyword> Keywords { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<Property> Properties { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Vote> Votes { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<ApplicationRole> Roles { get; set; }

        IDbSet<ApplicationUserRole> UserRoles { get; set; }

        IDbSet<Order> Orders { get; set; }

        IDbSet<OrderItem> OrderItems { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();
    }
}

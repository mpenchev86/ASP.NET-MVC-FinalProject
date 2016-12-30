﻿namespace MvcProject.Data.DbAccessConfig.Contexts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Models.Catalog;
    using Models.Identity;
    using Models.Media;
    using Models.Orders;
    using Models.Search;
    using MvcProject.Data.Models;

    public interface IMvcProjectDbContext : IDisposable
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

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();
    }
}

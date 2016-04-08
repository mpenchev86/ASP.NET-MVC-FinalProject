namespace MvcProject.Data.DbAccessConfig
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models;

    public interface IMvcProjectDbContext : IDisposable
    {
        IDbSet<Category> Categories { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Description> Descriptions { get; set; }

        IDbSet<Image> Images { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<Property> Properties { get; set; }

        //public IDbSet<ShippingInfo> ShippingInfoes { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Vote> Votes { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();
    }
}

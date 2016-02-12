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
        IDbSet<SampleProduct> SampleProducts { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}

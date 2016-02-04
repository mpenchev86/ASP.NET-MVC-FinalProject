namespace MvcProject.Data.DbAccessConfig
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class MvcProjectDbContext : IdentityDbContext<ApplicationUser>, IMvcProjectDbContext
    {
        public MvcProjectDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static MvcProjectDbContext Create()
        {
            return new MvcProjectDbContext();
        }

        public IDbSet<Tag> Tags { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}

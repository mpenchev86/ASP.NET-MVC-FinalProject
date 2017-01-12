namespace JustOrderIt.Web.Infrastructure.BackgroundWorkers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.GlobalConstants;

    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext()
            : base(DbAccess.HangfireConnectionStringName)
        {
            this.Database.CreateIfNotExists();
        }
    }
}

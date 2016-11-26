namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Common.GlobalConstants;
    using Hangfire;
    using Hangfire.Server;
    using Hangfire.SqlServer;
    using Infrastructure.BackgroundWorkers;
    using Owin;

    public class HangFireConfig
    {
        public static void Initialize(IAppBuilder app)
        {
            //var db = new HangfireDbContext();

            //GlobalConfiguration.Configuration.UseSqlServerStorage(DbAccess.HangfireConnectionStringName);

            app.UseHangfireDashboard("/hangfire");

            //GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            //// On application start, removes previously queued jobs, if any.
            //JobStorage.Current?.GetMonitoringApi()?.PurgeJobs();

            //app.UseHangfireServer();
        }
    }
}
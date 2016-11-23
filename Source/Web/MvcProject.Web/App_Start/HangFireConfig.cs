namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Common.GlobalConstants;
    using Hangfire;
    using Hangfire.SqlServer;
    using Owin;

    public class HangFireConfig
    {
        public static void Initialize(IAppBuilder app)
        {
            //GlobalConfiguration.Configuration.UseSqlServerStorage(DbAccess.ConnectionStringName);
            app.UseHangfireDashboard("/hangfire");
            //app.UseHangfireServer();
        }
    }
}
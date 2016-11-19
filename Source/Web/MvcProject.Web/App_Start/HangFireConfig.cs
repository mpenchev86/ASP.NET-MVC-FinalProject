namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Common.GlobalConstants;
    using Hangfire;
    using Owin;

    public class HangFireConfig
    {
        public static void Initialize(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(DbAccess.ConnectionStringName);

            // var dashboard = new DashboardOptions()
            // {
            //    AppPath = VirtualPathUtility.ToAbsolute("~/Public")
            // };

            // var server = new BackgroundJobServerOptions()
            // {
            //    Queues = new string[] { "critical", "default", "..." }
            // };

            app.UseHangfireDashboard("/hangfire"/*, dashboard*/);
            app.UseHangfireServer(/*server*/);
        }
    }
}
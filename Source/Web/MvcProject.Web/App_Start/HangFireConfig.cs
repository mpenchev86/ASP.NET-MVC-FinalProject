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
            //var sqlOptions = new SqlServerStorageOptions()
            //{
            //};

            GlobalConfiguration.Configuration.UseSqlServerStorage(DbAccess.ConnectionStringName/*, sqlOptions*/);

            // var dashboard = new DashboardOptions()
            // {
            //    AppPath = VirtualPathUtility.ToAbsolute("~/Public")
            // };

            // var serverOptions = new BackgroundJobServerOptions()
            // {
            //    Queues = new string[] { "critical", "default", "..." }
            // };

            app.UseHangfireDashboard("/hangfire"/*, dashboard*/);
            app.UseHangfireServer(/*serverOptions*/);
        }
    }
}
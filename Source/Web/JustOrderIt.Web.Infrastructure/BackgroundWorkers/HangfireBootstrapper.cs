namespace JustOrderIt.Web.Infrastructure.BackgroundWorkers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Hosting;
    using Common.GlobalConstants;
    using Hangfire;
    using Hangfire.Common;
    using Hangfire.SqlServer;
    using Newtonsoft.Json;
    using Serializers.HangfireJsonConverters;

    public class HangfireBootstrapper : IRegisteredObject
    {
        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();

        private readonly object lockObject = new object();
        private bool started;

        private BackgroundJobServer backgroundJobServer;

        private HangfireBootstrapper()
        {
        }

        public void Start(DbContext appDbContext)
        {
            lock (this.lockObject)
            {
                if (this.started)
                {
                    return;
                }

                this.started = true;

                HostingEnvironment.RegisterObject(this);

                var db = new HangfireDbContext();

                GlobalConfiguration.Configuration.UseSqlServerStorage(DbAccess.HangfireConnectionStringName);
                GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

                // On application start, removes previously queued and unfinished/interrupted jobs with "Processing" or "Scheduled" status, if any.
                JobStorage.Current?.GetMonitoringApi()?.PurgeJobs(appDbContext);

                var serverOptions = new BackgroundJobServerOptions()
                {
                    ServerName = ConfigurationManager.AppSettings["HangfireServerName"]
                };

                this.backgroundJobServer = new BackgroundJobServer(serverOptions);
            }
        }

        public void Stop()
        {
            lock (this.lockObject)
            {
                if (this.backgroundJobServer != null)
                {
                    this.backgroundJobServer.Dispose();
                }

                HostingEnvironment.UnregisterObject(this);
            }
        }

        void IRegisteredObject.Stop(bool immediate)
        {
            this.Stop();
        }
    }
}

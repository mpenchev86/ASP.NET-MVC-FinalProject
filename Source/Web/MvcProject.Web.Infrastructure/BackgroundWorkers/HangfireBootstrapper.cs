namespace MvcProject.Web.Infrastructure.BackgroundWorkers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Hosting;
    using Common.GlobalConstants;
    using Hangfire;
    using Hangfire.SqlServer;

    public class HangfireBootstrapper : IRegisteredObject
    {
        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();

        private readonly object lockObject = new object();
        private bool started;

        private BackgroundJobServer backgroundJobServer;

        private HangfireBootstrapper()
        {
        }

        public void Start()
        {
            lock (this.lockObject)
            {
                if (this.started)
                {
                    return;
                }

                this.started = true;

                HostingEnvironment.RegisterObject(this);

                GlobalConfiguration.Configuration.UseSqlServerStorage(
                    DbAccess.ConnectionStringName);

                GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

                // On application start, removes previously queued jobs.
                JobStorage.Current?.GetMonitoringApi()?.PurgeJobs();

                this.backgroundJobServer = new BackgroundJobServer();
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

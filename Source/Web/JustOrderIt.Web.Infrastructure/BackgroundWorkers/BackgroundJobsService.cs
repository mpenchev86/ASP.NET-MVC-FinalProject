namespace JustOrderIt.Web.Infrastructure.BackgroundWorkers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hangfire;
    using Hangfire.Client;

    public class BackgroundJobsService : IBackgroundJobsService
    {
        public IBackgroundJobClient JobClient
        {
            get { return new BackgroundJobClient(); }
        }
    }
}

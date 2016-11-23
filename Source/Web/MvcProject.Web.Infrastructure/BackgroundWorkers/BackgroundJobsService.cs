namespace MvcProject.Web.Infrastructure.BackgroundWorkers
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
        public BackgroundJobClient JobClient
        {
            get { return new BackgroundJobClient(); }
        }

        public void MethodName()
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine(), "* 5 * * *");
        }
    }
}

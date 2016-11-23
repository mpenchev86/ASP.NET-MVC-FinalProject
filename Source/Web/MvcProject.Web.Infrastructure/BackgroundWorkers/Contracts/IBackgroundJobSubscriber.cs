namespace MvcProject.Web.Infrastructure.BackgroundWorkers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hangfire;

    public interface IBackgroundJobSubscriber
    {
        void BackgroundOperation(IJobCancellationToken token, params object[] args);
    }
}

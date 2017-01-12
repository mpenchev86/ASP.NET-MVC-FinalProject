namespace JustOrderIt.Web.Infrastructure.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Hosting;
    using BackgroundWorkers;

    public class PreWarmCache : IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            //HangfireBootstrapper.Instance.Start();
        }
    }
}

namespace JustOrderIt.Web.Infrastructure.Caching
{
    using System.Web.Hosting;
    using BackgroundWorkers;

    public class PreWarmCache : IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            // HangfireBootstrapper.Instance.Start();
        }
    }
}

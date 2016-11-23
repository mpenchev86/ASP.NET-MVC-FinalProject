namespace MvcProject.Services.Web.CacheServices
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Hosting;
    using Hangfire;
    using MvcProject.Web.Infrastructure.BackgroundWorkers;

    public class BaseHttpCacheService : ICacheService
    {
        protected static readonly object LockObject = new object();

        public T Get<T>(string key, Func<T> dataFunc, int absoluteExpiration)
        {
            if (HttpRuntime.Cache[key] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[key] == null)
                    {
                        var data = dataFunc();
                        HttpRuntime.Cache.Insert(key, data, null, DateTime.UtcNow.AddSeconds(absoluteExpiration), Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T)HttpRuntime.Cache[key];
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
    }
}

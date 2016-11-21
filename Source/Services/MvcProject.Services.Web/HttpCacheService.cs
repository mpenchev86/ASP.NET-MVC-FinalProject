namespace MvcProject.Services.Web
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
    using ServiceModels;

    public class HttpCacheService : ICacheService
    {
        private static readonly object LockObject = new object();

        private static ConcurrentDictionary<string, CacheProfile> cacheProfiles =
             new ConcurrentDictionary<string, CacheProfile>();

        public static ConcurrentDictionary<string, CacheProfile> CacheProfiles
        {
            get { return cacheProfiles; }
        }

        //public bool HasBackgroundJobAssigned(string key)
        //{
        //    if (!cacheProfiles.Keys.Any(k => k == key))
        //    {
        //        throw new ArgumentException("The provided cache profile key is invalid.", "key");
        //    }

        //    if (cacheProfiles[key].HasBackgroundJobAssigned)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public void AddOrUpdateCacheProfile(string key, CacheProfile cacheProfile)
        //{
        //    cacheProfiles.AddOrUpdate(key, cacheProfile, (k, i) => i);
        //}

        public T Get<T, TClass>(object[] methodArguments, string key, Func<T> dataFunc, int absoluteExpiration, int updateAbsoluteExp, CacheItemPriority priority = CacheItemPriority.Default)
            where TClass : class, IBackgroundJobSubscriber
        {
            return this.InsertInCache<T, TClass>(methodArguments, key, dataFunc, absoluteExpiration, updateAbsoluteExp, priority);
        }

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

        private T InsertInCache<T, TClass>(object[] methodArguments, string key, Func<T> dataFunc, int absoluteExpiration, int updateAbsoluteExp, CacheItemPriority priority)
            where TClass : class, IBackgroundJobSubscriber
        {
            if (HttpRuntime.Cache[key] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[key] == null)
                    {
                        T data;
                        if (cacheProfiles.Keys.Any(k => k == key) && cacheProfiles[key].UpdatedValueFlag == true)
                        {
                            data = (T)cacheProfiles[key].UpdatedCacheValue;
                        }
                        else
                        {
                            data = dataFunc();
                        }

                        var cacheSettings = cacheProfiles.AddOrUpdate(
                            key,
                            new CacheProfile()
                            {
                                UpdatedCacheValue = data,
                                MethodArguments = methodArguments,
                                AbsoluteExpiration = absoluteExpiration,
                                UpdateDelay = updateAbsoluteExp,
                                //HasBackgroundJobAssigned = false,
                                UpdatedValueFlag = false
                            },
                            (k, d) => d);

                        HttpRuntime.Cache.Insert(
                            key,
                            data,
                            null,
                            DateTime.UtcNow.AddSeconds(absoluteExpiration),
                            Cache.NoSlidingExpiration,
                            new CacheItemUpdateCallback(OnUpdate<T, TClass>)
                            );

                        BackgroundJob.Schedule<TClass>(x => x.BackgroundOperation(methodArguments), TimeSpan.FromSeconds(updateAbsoluteExp));
                    }
                }
            }

            return (T)HttpRuntime.Cache[key];
        }

        private static void OnUpdate<T, TClass>(
            string key,
            CacheItemUpdateReason reason,
            out object expensiveObject,
            out CacheDependency dependency,
            out DateTime absoluteExpiration,
            out TimeSpan slidingExpiration)
            where TClass : class, IBackgroundJobSubscriber
        {
            expensiveObject = (T)cacheProfiles[key].UpdatedCacheValue;
            dependency = null;
            absoluteExpiration = DateTime.UtcNow.AddSeconds(cacheProfiles[key].AbsoluteExpiration);
            slidingExpiration = Cache.NoSlidingExpiration;

            //cacheProfiles[key].HasBackgroundJobAssigned = false;

            BackgroundJob.Schedule<TClass>(x => x.BackgroundOperation(cacheProfiles[key].MethodArguments), TimeSpan.FromSeconds(cacheProfiles[key].UpdateDelay));
        }
    }
}

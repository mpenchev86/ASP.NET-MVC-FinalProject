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
    using ServiceModels;

    public class HttpCacheService : ICacheService
    {
        private static readonly object LockObject = new object();

        private static ConcurrentDictionary<string, CacheSettings> cacheSettingsCollection =
             new ConcurrentDictionary<string, CacheSettings>();

        //private static ConcurrentDictionary<string, object> updatedCacheValues =
        //     new ConcurrentDictionary<string, object>();

        //private static ConcurrentDictionary<string, bool> updatedValueFlags =
        //     new ConcurrentDictionary<string, bool>();

        //private static ConcurrentDictionary<string, Delegate> expensiveOperations =
        //     new ConcurrentDictionary<string, Delegate>();

        //private static ConcurrentDictionary<string, CacheItemUpdateCallback> onUpdateCallbacks =
        //     new ConcurrentDictionary<string, CacheItemUpdateCallback>();

        public T Get<T>(string key, Func<T>/*Delegate*/ dataFunc/*, Type delegType*/, int absoluteExpiration, int updateAbsoluteExp, CacheItemPriority priority = CacheItemPriority.Default)
        {
            return this.InsertInCache(key, dataFunc/*, delegType*/, absoluteExpiration, updateAbsoluteExp, priority);

            //return (T)HttpRuntime.Cache[itemName];
        }

        public T Get<T>(string key, Func<T> dataFunc, int absoluteExpiration)
        {
            if (HttpRuntime.Cache[key] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[key] == null)
                    {
                        HttpRuntime.Cache.Insert(key, dataFunc(), null, DateTime.UtcNow.AddSeconds(absoluteExpiration), Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T)HttpRuntime.Cache[key];
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        private T InsertInCache<T>(string key, Func<T>/*Delegate*/ dataFunc/*, Type delegType*/, int absoluteExpiration, int updateAbsoluteExp, CacheItemPriority priority)
        {
            if (HttpRuntime.Cache[key] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[key] == null)
                    {
                        T data;
                        //if (updatedValueFlags.Keys.Any(k => k == key) && updatedValueFlags[key] == true)
                        //{
                        //    data = (T)updatedCacheValues[key];
                        //    updatedValueFlags[key] = false;
                        //}
                        //else
                        //{
                        //    data = dataFunc();
                        //}

                        if (cacheSettingsCollection.Keys.Any(k => k == key) && cacheSettingsCollection[key].UpdatedValueFlag == true)
                        {
                            data = (T)cacheSettingsCollection[key].UpdatedCacheValue;
                            cacheSettingsCollection[key].UpdatedValueFlag = false;
                        }
                        else
                        {
                            data = dataFunc();
                        }

                        var cacheSettings = cacheSettingsCollection.AddOrUpdate(
                            key,
                            new CacheSettings()
                            {
                                ExpensiveOperation = dataFunc,
                                OnUpdateCallback = new CacheItemUpdateCallback(OnUpdate<T>),
                                UpdatedCacheValue = data,
                                AbsoluteExpiration = absoluteExpiration,
                                UpdateDelay = updateAbsoluteExp,
                                UpdatedValueFlag = false
                            },
                            (k, d) => d);

                        HttpRuntime.Cache.Insert(key, data, null, DateTime.UtcNow.AddSeconds(absoluteExpiration), Cache.NoSlidingExpiration, cacheSettings.OnUpdateCallback);

                        //expensiveOperations.AddOrUpdate(key, dataFunc, (k, d) => d);
                        //var updateDeleg = onUpdateCallbacks.GetOrAdd(key, new CacheItemUpdateCallback(OnUpdate<T>));

                        //HttpRuntime.Cache.Insert(key, data, null, DateTime.UtcNow.AddSeconds(absoluteExpiration), Cache.NoSlidingExpiration, updateDeleg);

                        //var removeDeleg = onRemoveCallbacks.GetOrAdd(itemName, new CacheItemRemovedCallback(this.OnRemove<T>));
                        //HttpRuntime.Cache.Insert(itemName, data, null, DateTime.UtcNow.AddSeconds(absoluteExpiration), Cache.NoSlidingExpiration, priority, removeDeleg);

                        //HttpRuntime.Cache.Insert(key, data, null, DateTime.UtcNow.AddSeconds(absoluteExpiration), Cache.NoSlidingExpiration);

                        var jobId = BackgroundJob.Schedule(
                            () => GetExpensiveData<T>(
                                JobCancellationToken.Null,
                                data,
                                key),
                            TimeSpan.FromSeconds(cacheSettings.UpdateDelay)
                            );
                    }
                }
            }

            return (T)HttpRuntime.Cache[key];
        }

        public static void OnUpdate<T>(
            string key,
            CacheItemUpdateReason reason,
            out object expensiveObject,
            out CacheDependency dependency,
            out DateTime absoluteExpiration,
            out TimeSpan slidingExpiration)
        {
            expensiveObject = (T)cacheSettingsCollection[key].UpdatedCacheValue;
            dependency = null;
            absoluteExpiration = DateTime.UtcNow.AddSeconds(cacheSettingsCollection[key].AbsoluteExpiration);
            slidingExpiration = Cache.NoSlidingExpiration;

            //HostingEnvironment.QueueBackgroundWorkItem(ct => GetExpensiveData<T>((T)cacheSettingsCollection[key].UpdatedCacheValue, key));

            var jobId = BackgroundJob.Schedule(
                () => GetExpensiveData<T>(
                    JobCancellationToken.Null,
                    (T)cacheSettingsCollection[key].UpdatedCacheValue,
                    key),
                TimeSpan.FromSeconds(cacheSettingsCollection[key].UpdateDelay)
                );
        }

        //[AutomaticRetry(Attempts = 0)]
        public static void GetExpensiveData<T>(IJobCancellationToken cancellationToken, T data, string key)
        {
            if (!cacheSettingsCollection.Keys.Any(k => k == key))
            {
                return;
            }

            cacheSettingsCollection[key].UpdatedCacheValue = ((Func<T>)cacheSettingsCollection[key].ExpensiveOperation)();
            cacheSettingsCollection[key].UpdatedValueFlag = true;

            //updatedCacheValues.AddOrUpdate(key, ((Func<T>)expensiveOperations[key])(), (k, d) => d);
            //updatedValueFlags[key] = true;

            //cancellationToken.ThrowIfCancellationRequested();
        }
    }
}

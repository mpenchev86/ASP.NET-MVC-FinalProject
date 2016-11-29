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
    using Hangfire;
    using MvcProject.Web.Infrastructure.BackgroundWorkers;

    public class AutoUpdateCacheService/*<TClass>*/ : BaseHttpCacheService, IAutoUpdateCacheService/*<TClass>*/
        //where TClass : class, IBackgroundJobSubscriber
    {
        private static ConcurrentDictionary<string, CacheProfile/*<TClass>*/> auxiliaryCacheProfiles =
             new ConcurrentDictionary<string, CacheProfile/*<TClass>*/>();

        private readonly IBackgroundJobsService backgroundService;

        public AutoUpdateCacheService(IBackgroundJobsService backgroundService)
        {
            this.backgroundService = backgroundService;
        }

        public void UpdateAuxiliaryCacheValue(string key, object updatedData)
        {
            if (!auxiliaryCacheProfiles.Keys.Any(k => k == key))
            {
                throw new ArgumentException("There is no CacheProfile in the dictionary corresponding to the provided key.", "key");
            }

            this.AddOrUpdateCacheProfile(
                key,
                updatedData,
                auxiliaryCacheProfiles[key].MethodName,
                auxiliaryCacheProfiles[key].MethodArguments,
                auxiliaryCacheProfiles[key].AbsoluteExpiration,
                auxiliaryCacheProfiles[key].UpdateJobDelay,
                true);
        }

        public T Get<T, TClass>(string key, Func<T> dataFunc, int absoluteExpiration, string methodName /*Expression<Func<TClass, Task>>*//*Action<object[]>*/ /*worker*/, object[]/*Expression<Func<TClass, Task>>*/ methodArguments, int updateJobDelay, CacheItemPriority priority = CacheItemPriority.Default)
            where TClass : class, IBackgroundJobSubscriber
        {
            if (HttpRuntime.Cache[key] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[key] == null)
                    {
                        T data;
                        if (auxiliaryCacheProfiles.Keys.Any(k => k == key) && auxiliaryCacheProfiles[key].UpdatedValueFlag == true)
                        {
                            data = (T)auxiliaryCacheProfiles[key].UpdatedCacheValue;
                        }
                        else
                        {
                            data = dataFunc();
                        }

                        this.AddOrUpdateCacheProfile(key, data, methodName /*worker*/, methodArguments, absoluteExpiration, updateJobDelay, false);
                        HttpRuntime.Cache.Insert(key, data, null, DateTime.UtcNow.AddSeconds(absoluteExpiration), Cache.NoSlidingExpiration, new CacheItemUpdateCallback(this.OnUpdate<T, TClass>));

                        //BackgroundJobServer
                        this.backgroundService.JobClient.Schedule<TClass>(x => x.BackgroundOperation(methodName, methodArguments) /*methodArguments*//*worker*/, TimeSpan.FromSeconds(updateJobDelay));
                    }
                }
            }

            return (T)HttpRuntime.Cache[key];
        }

        private void OnUpdate<T, TClass>(
            string key,
            CacheItemUpdateReason reason,
            out object expensiveObject,
            out CacheDependency dependency,
            out DateTime absoluteExpiration,
            out TimeSpan slidingExpiration)
            where TClass : class, IBackgroundJobSubscriber
        {
            expensiveObject = (T)auxiliaryCacheProfiles[key].UpdatedCacheValue;
            dependency = null;
            absoluteExpiration = DateTime.UtcNow.AddSeconds(auxiliaryCacheProfiles[key].AbsoluteExpiration);
            slidingExpiration = Cache.NoSlidingExpiration;

            this.backgroundService.JobClient.Schedule<TClass>(x => x.BackgroundOperation(auxiliaryCacheProfiles[key].MethodName, auxiliaryCacheProfiles[key].MethodArguments), TimeSpan.FromSeconds(auxiliaryCacheProfiles[key].UpdateJobDelay));
        }

        private void AddOrUpdateCacheProfile/*<TClass>*/(string key, object updatedCacheValue, string methodName /*Expression<Func<TClass, Task>>*/ /*Action<object[]>*/ /*worker*/, object[]/* Expression<Func<TClass, Task>>*/ methodArguments, int absoluteExpiration, int updateJobDelay, bool updatedValueFlag)
            //where TClass : class, IBackgroundJobSubscriber
        {
            var cacheProfile = new CacheProfile/*<TClass>*/()
            {
                UpdatedCacheValue = updatedCacheValue,
                MethodName = methodName,
                //Method = worker,
                MethodArguments = methodArguments,
                AbsoluteExpiration = absoluteExpiration,
                UpdateJobDelay = updateJobDelay,
                UpdatedValueFlag = updatedValueFlag
            };

            var cacheSettings = auxiliaryCacheProfiles.AddOrUpdate(key, cacheProfile, (k, d) => cacheProfile);
        }
    }
}

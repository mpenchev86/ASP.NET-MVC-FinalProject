namespace MvcProject.Services.Web.CacheServices
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Caching;
    using Hangfire;
    using MvcProject.Web.Infrastructure.BackgroundWorkers;

    public class AutoUpdateCacheService : BaseHttpCacheService, IAutoUpdateCacheService
    {
        private static ConcurrentDictionary<string, CacheProfile> auxiliaryCacheProfiles =
             new ConcurrentDictionary<string, CacheProfile>();

        public void UpdateAuxiliaryCacheValue(string key, object updatedData)
        {
            if (!auxiliaryCacheProfiles.Keys.Any(k => k == key))
            {
                throw new ArgumentException("There is no CacheProfile in the dictionary corresponding to the provided key.", "key");
            }

            //var profile = new CacheProfile()
            //{
            //    UpdatedCacheValue = updatedData,
            //    MethodArguments = auxiliaryCacheProfiles[key].MethodArguments,
            //    AbsoluteExpiration = auxiliaryCacheProfiles[key].AbsoluteExpiration,
            //    UpdateDelay = auxiliaryCacheProfiles[key].UpdateDelay,
            //    UpdatedValueFlag = true,
            //};

            //auxiliaryCacheProfiles.AddOrUpdate(key, profile, (k, p) => profile);

            this.AddOrUpdateCacheProfile(
                key,
                updatedData,
                auxiliaryCacheProfiles[key].MethodArguments,
                auxiliaryCacheProfiles[key].AbsoluteExpiration,
                auxiliaryCacheProfiles[key].UpdateJobDelay,
                true);
        }

        public T Get<T, TClass>(object[] methodArguments, string key, Func<T> dataFunc, int absoluteExpiration, int updateJobDelay, CacheItemPriority priority = CacheItemPriority.Default)
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

                        //var cacheProfile = new CacheProfile()
                        //{
                        //    UpdatedCacheValue = data,
                        //    MethodArguments = methodArguments,
                        //    AbsoluteExpiration = absoluteExpiration,
                        //    UpdateDelay = updateAbsoluteExp,
                        //    UpdatedValueFlag = false
                        //};

                        //auxiliaryCacheProfiles.AddOrUpdate(key, cacheProfile, (k, d) => cacheProfile);

                        this.AddOrUpdateCacheProfile(key, data, methodArguments, absoluteExpiration, updateJobDelay, false);
                        HttpRuntime.Cache.Insert(key, data, null, DateTime.UtcNow.AddSeconds(absoluteExpiration), Cache.NoSlidingExpiration, new CacheItemUpdateCallback(OnUpdate<T, TClass>));
                        BackgroundJob.Schedule<TClass>(x => x.BackgroundOperation(JobCancellationToken.Null, methodArguments), TimeSpan.FromSeconds(updateJobDelay));
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
            expensiveObject = (T)auxiliaryCacheProfiles[key].UpdatedCacheValue;
            dependency = null;
            absoluteExpiration = DateTime.UtcNow.AddSeconds(auxiliaryCacheProfiles[key].AbsoluteExpiration);
            slidingExpiration = Cache.NoSlidingExpiration;

            BackgroundJob.Schedule<TClass>(x => x.BackgroundOperation(JobCancellationToken.Null, auxiliaryCacheProfiles[key].MethodArguments), TimeSpan.FromSeconds(auxiliaryCacheProfiles[key].UpdateJobDelay));
        }

        private void AddOrUpdateCacheProfile(string key, object updatedCacheValue, object[] methodArguments, int absoluteExpiration, int updateJobDelay, bool updatedValueFlag)
        {
            var cacheProfile = new CacheProfile()
            {
                UpdatedCacheValue = updatedCacheValue,
                MethodArguments = methodArguments,
                AbsoluteExpiration = absoluteExpiration,
                UpdateJobDelay = updateJobDelay,
                UpdatedValueFlag = updatedValueFlag
            };

            var cacheSettings = auxiliaryCacheProfiles.AddOrUpdate(key, cacheProfile, (k, d) => cacheProfile);
        }
    }
}

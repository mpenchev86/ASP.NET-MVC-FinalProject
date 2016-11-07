namespace MvcProject.Services.Web
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Caching;

    public class HttpCacheService : ICacheService
    {
        private static readonly object LockObject = new object();
        private static Delegate expensiveObject;
        private static int callbackAbsoluteExp;
        //private static string callbackKey;
        private static ConcurrentDictionary<string, CacheItemUpdateCallback> updateCallbacks =
             new ConcurrentDictionary<string, CacheItemUpdateCallback>();

        public T Get<T>(string itemName, Func<T> dataFunc, int absoluteExpiration = 0, bool hasUpdateCallback = false, int updateAbsoluteExp = 0)
        {
            if (HttpRuntime.Cache[itemName] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[itemName] == null)
                    {
                        var data = dataFunc();
                        CacheItemUpdateCallback deleg = null;
                        if (hasUpdateCallback)
                        {
                            expensiveObject = dataFunc;
                            updateAbsoluteExp = callbackAbsoluteExp;
                            //callbackKey = itemName;
                            deleg = updateCallbacks.GetOrAdd(itemName, new CacheItemUpdateCallback(SetUpdateDelegate<T>));
                        }

                        HttpRuntime.Cache.Insert(
                            itemName,
                            data,
                            null,
                            DateTime.Now.AddSeconds(absoluteExpiration),
                            Cache.NoSlidingExpiration
                            //,updateCallbacks[itemName]
                            , deleg
                            );
                    }
                }
            }

            return (T)HttpRuntime.Cache[itemName];
        }

        public void Remove(string itemName)
        {
            HttpRuntime.Cache.Remove(itemName);
        }

        private static void SetUpdateDelegate<T>(
            string key,
            CacheItemUpdateReason reason,
            out object expensiveObject,
            out CacheDependency dependency,
            out DateTime absoluteExpiration,
            out TimeSpan slidingExpiration)
        {
            //key = callbackKey;
            //reason = CacheItemUpdateReason.Expired;
            expensiveObject = GetExpensiveObject<T>(key);
            dependency = null;
            absoluteExpiration = DateTime.Now.AddSeconds(callbackAbsoluteExp);
            slidingExpiration = Cache.NoSlidingExpiration;
        }

        private static T GetExpensiveObject<T>(string key)
        {
            return ((Func<T>)expensiveObject)();
        }
    }
}

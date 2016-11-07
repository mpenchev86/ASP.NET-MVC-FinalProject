namespace MvcProject.Web.Infrastructure.Caching
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class CacheConcurrentDictionary
    {
        private static ConcurrentDictionary<string, object> cache = new ConcurrentDictionary<string, object>();

        private static ConcurrentDictionary<MethodInfo, IEnumerable<object>> cacheMetaData = new ConcurrentDictionary<MethodInfo, IEnumerable<object>>();

        public static void GetOrAdd(string key, object value)
        {
            cache.GetOrAdd(key, value);
        }

        public static void GetOrAdd(string key, Func<string, object> value)
        {
            cache.GetOrAdd(key, value);
        }

        public static void GetOrAdd(MethodInfo key, IEnumerable<object> value)
        {
            cacheMetaData.GetOrAdd(key, value);
        }

        public static void GetOrAdd(MethodInfo key, Func<MethodInfo, IEnumerable<object>> value)
        {
            cacheMetaData.GetOrAdd(key, value);
        }
    }
}

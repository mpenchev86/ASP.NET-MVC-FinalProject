namespace JustOrderIt.Services.Web.CacheServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Caching;
    using JustOrderIt.Web.Infrastructure.BackgroundWorkers;

    public interface IAutoUpdateCacheService : ICacheService
    {
        /// <summary>
        /// Gets the cached item with the specified key. If none is found, a new one is inserted into the cache using
        /// the specified parameters and then it is returned to the caller. It also defines a background job that adds or updates
        /// a temporary cache value into a static dictionary, and a update callback to update the runtime Cache object.
        /// </summary>
        /// <typeparam name="T">The type of the data item being cached.</typeparam>
        /// <typeparam name="TSubscriber">The type of the class, implementing a background job operation.</typeparam>
        /// <param name="cacheKey">The key of the cached data item.</param>
        /// <param name="dataFunc">The delegate providing the logic for retrieving the data object.</param>
        /// <param name="absoluteExpiration">Expiration time for the cached data item (in seconds).</param>
        /// <param name="methodName">The name of the worker method used in a background operation.</param>
        /// <param name="methodArguments">An object array containing the method arguments for the worker method used in a background operation.</param>
        /// <param name="updateJobDelay">The delay for the scheduled update background job (in seconds).</param>
        /// <param name="priority">Specifies the priority of the cached item. The garbage collector cleans lower priority
        /// cache items first.</param>
        /// <returns>The original data retrieved from the cached data object.</returns>
        T Get<T, TSubscriber>(string cacheKey, Func<T> dataFunc, int absoluteExpiration, string methodName, object[] methodArguments, int updateJobDelay, CacheItemPriority priority = CacheItemPriority.Default)
            where TSubscriber : class, IBackgroundJobSubscriber;

        /// <summary>
        /// Supplies an updated cache value to a cache profile item in the auxiliary dictionary.
        /// </summary>
        /// <param name="key">The cache object key.</param>
        /// <param name="updatedData">The updated cache object value which will be picked by the cache
        /// update callback and stored in the ASP.NET HttpRuntime Cache.</param>
        void UpdateAuxiliaryCacheValue(string key, object updatedData);
    }
}

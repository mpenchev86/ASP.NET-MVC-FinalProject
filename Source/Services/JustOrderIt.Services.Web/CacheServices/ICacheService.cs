namespace JustOrderIt.Services.Web.CacheServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Caching;
    using JustOrderIt.Web.Infrastructure.BackgroundWorkers;

    public interface ICacheService
    {
        /// <summary>
        /// Gets the cached item with the specified key. If none is found, a new one is inserted into the cache using
        /// the specified parameters and then it is returned to the caller.
        /// </summary>
        /// <typeparam name="T">The type of the data item being cached.</typeparam>
        /// <param name="key">The name of the cached data item.</param>
        /// <param name="dataFunc">The delegate providing the logic for retrieving the data object.</param>
        /// <param name="absoluteExpiration">Expiration time for the cached data item (in seconds).</param>
        /// <returns>The original data retrieved from the cached data object.</returns>
        T Get<T>(string key, Func<T> dataFunc, int absoluteExpiration);

        /// <summary>
        /// Removes from cache the item with the specified key.
        /// </summary>
        /// <param name="key">The key used to identify the cached item.</param>
        void Remove(string key);
    }
}

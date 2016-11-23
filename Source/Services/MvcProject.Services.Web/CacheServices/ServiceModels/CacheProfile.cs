namespace MvcProject.Services.Web.CacheServices
{
    /// <summary>
    /// A data store class used to keep information necessary for performing seamless cache updates via a background processing service.
    /// </summary>
    public class CacheProfile
    {
        /// <summary>
        /// Gets or sets the updated cache value which is to be used by an update cache callback to repopulate the HttpRuntime Cache.
        /// </summary>
        /// <value>
        /// The updated cache value which is to be used by an update cache callback to repopulate the HttpRuntime Cache.
        /// </value>
        public object UpdatedCacheValue { get; set; }

        /// <summary>
        /// Gets or sets the arguments array passed to the background operation method.
        /// </summary>
        /// <value>
        /// The arguments array passed to the background operation method.
        /// </value>
        public object[] MethodArguments { get; set; }

        /// <summary>
        /// Gets or sets the expiration time of the cache item (in seconds).
        /// </summary>
        /// <value>
        /// The expiration time of the cache item (in seconds).
        /// </value>
        public int AbsoluteExpiration { get; set; }

        /// <summary>
        /// Gets or sets the delay before a background job fetches the updated data (in seconds).
        /// </summary>
        /// <value>
        /// The delay before a background job fetches the updated data (in seconds).
        /// </value>
        public int UpdateJobDelay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the cache value property has a value that can be supplied to the HttpRuntime Cache.
        /// Used primarily as a fail-safe in case a cache profile was created but it was not provided with an updated cache value.
        /// </summary>
        /// <value>
        /// A value indicating whether the cache value property has a value that can be supplied to the HttpRuntime Cache.
        /// Used primarily as a fail-safe in case a cache profile was created but it was not provided with an updated cache value.
        /// </value>
        public bool UpdatedValueFlag { get; set; }
    }
}

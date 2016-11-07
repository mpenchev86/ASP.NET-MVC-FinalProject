namespace MvcProject.Common.GlobalConstants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CacheConstants
    {
        // Public/HomeController/Index ListView
        public const int IndexListViewCacheExpirationInSeconds = 30 * 60;
        public const int IndexCarouselCacheExpirationInSeconds = 30 * 60;
    }
}

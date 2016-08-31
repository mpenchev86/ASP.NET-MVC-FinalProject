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
        public const int IndexListViewCacheDurationInSeconds = 1 * 24/* * 60 * 60*/;
        public const int IndexCarouselCacheDurationInSeconds = 1 * 24/* * 60 * 60*/;
    }
}

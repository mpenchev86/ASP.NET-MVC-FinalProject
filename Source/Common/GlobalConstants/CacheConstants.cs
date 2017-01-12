namespace JustOrderIt.Common.GlobalConstants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CacheConstants
    {
        // Public/HomeController/Index Carousel
        public const int CarouselDataCacheExpiration = 30 * 60;

        // Public/HomeController/Index ListView
        public const int IndexListViewCacheExpiration = 3 * 60;

        // Public/SearchController/ReadSearchResult
        public const int AllProductsInCategoryCacheExpiration = 5 * 60;
        public const int AllProductsInCategoryUpdateBackgroundJobDelay = 3 * 60;

        // Public/SearchController/SearchAutoComplete
        public const int KeywordsForAutoCompleteCacheExpiration = 5 * 60;
        public const int KeywordsForAutoCompleteUpdateBackgroundJobDelay = 3 * 60;
    }
}

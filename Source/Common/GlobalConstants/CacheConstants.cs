namespace JustOrderIt.Common.GlobalConstants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CacheConstants
    {
        //// Public/HomeController/Index Carousel
        public const int CarouselDataCacheExpiration = 30 * 60;

        //// Public/HomeController/Index ListView
        public const int IndexListViewCacheExpiration = 3 * 60;

        //// Public/SearchController/ReadSearchResult
        public const int AllProductsInCategoryCacheExpiration = 15 * 60;

        /// <remarks>
        /// The margin between the update delay period and the cache expiration period should be proportionate to the
        /// number of products being cached.
        /// </remarks>
        public const int AllProductsInCategoryUpdateBackgroundJobDelay = 11 * 60;

        //// Public/SearchController/SearchAutoComplete
        public const int KeywordsForAutoCompleteCacheExpiration = 15 * 60;
        public const int KeywordsForAutoCompleteUpdateBackgroundJobDelay = 13 * 60;

        //// Cache keys
        public const string AllCategoriesKeywords = "allCategoriesKeywords";
    }
}

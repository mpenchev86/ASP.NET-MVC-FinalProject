namespace MvcProject.Common.GlobalConstants
{
    public class AppSpecificConstants
    {
        public const string UserNameDeletedUser = "DELETED";

        // Public/HomeController/Index ListView
        public const int IndexListViewNumberOfNewestProducts = 100;
        public const int IndexListViewNumberOfBestSellingProducts = 100;
        public const int IndexListViewNumberOfhighestVotedProducts = 100;
        public const int IndexListViewCacheDurationInSeconds = 1 * 24/* * 60 * 60*/;
        public const int IndexCarouselCacheDurationInSeconds = 1 * 24/* * 60 * 60*/;
    }
}

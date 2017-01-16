namespace JustOrderIt.Web.Infrastructure.HtmlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using Common.GlobalConstants;

    public enum ImageSizes
    {
        Small,
        Thumbnail,
        Large
    }

    public static class UrlExtensions
    {
        public static string ProductPicture(this UrlHelper helper, string urlPath, string imageExtension, ImageSizes size)
        {
            string imageSuffix = string.Empty;
            switch (size)
            {
                case ImageSizes.Small: imageSuffix = StaticResourcesUrls.ImageSmallSizeSuffix; break;
                case ImageSizes.Thumbnail: imageSuffix = StaticResourcesUrls.ImageThumbnailSuffix; break;
                case ImageSizes.Large: imageSuffix = StaticResourcesUrls.ImageHighResolutionSuffix; break;
                //default: imageSuffix = string.Empty; break;
            }

            return helper.Content(GetImageIfExists(string.Format(StaticResourcesUrls.ServerPathDataItemsImages, urlPath, imageSuffix, imageExtension), imageSuffix));
        }

        /// <summary>
        /// Serves App Management Images
        /// </summary>
        /// <param name="helper">UrlHelper class instance.</param>
        /// <param name="fileName">Name + extension of the file</param>
        /// <returns>The absolute path to the file.</returns>
        public static string AppMngContent(this UrlHelper helper, string fileName)
        {
            return helper.Content(GetImageIfExists(StaticResourcesUrls.ServerPathAppManagementImages + fileName, string.Empty));
        }

        private static string GetImageIfExists(string path, string suffix)
        {
            var physicalPath = HostingEnvironment.MapPath(path);
            if (File.Exists(physicalPath))
            {
                return path;
            }
            else
            {
                return Path.Combine(StaticResourcesUrls.ServerPathAppManagementImages, string.Format(StaticResourcesUrls.ImageNotFoundFileName, suffix));
            }
        }
    }
}

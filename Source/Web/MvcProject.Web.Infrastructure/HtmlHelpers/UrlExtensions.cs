namespace MvcProject.Web.Infrastructure.HtmlHelpers
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

    public static class UrlExtensions
    {
        public static string ProductTmblPicture(this UrlHelper helper, string urlPath, string imageExtension)
        {
            return helper.Content(GetImageIfExists(string.Format(StaticResourcesUrls.ServerPathDataItemsImages, urlPath, StaticResourcesUrls.ImageThumbnailSuffix, imageExtension)));
        }

        public static string ProductHiRezPicture(this UrlHelper helper, string urlPath, string imageExtension)
        {
            return helper.Content(GetImageIfExists(string.Format(StaticResourcesUrls.ServerPathDataItemsImages, urlPath, StaticResourcesUrls.HighResolutionImageSuffix, imageExtension)));
        }

        /// <summary>
        /// Serves App Management Images
        /// </summary>
        /// <param name="helper">UrlHelper class instance.</param>
        /// <param name="fileName">Name + extension of the file</param>
        /// <returns>The absolute path to the file.</returns>
        public static string AppMngContent(this UrlHelper helper, string fileName)
        {
            return helper.Content(GetImageIfExists(StaticResourcesUrls.ServerPathAppManagementImages + fileName));
        }

        public static string GetImageIfExists(string path)
        {
            var physicalPath = HostingEnvironment.MapPath(path);
            if (File.Exists(physicalPath))
            {
                return path;
            }
            else
            {
                return Path.Combine(StaticResourcesUrls.ServerPathAppManagementImages, StaticResourcesUrls.ImageNotFoundFileName);
            }
        }
    }
}

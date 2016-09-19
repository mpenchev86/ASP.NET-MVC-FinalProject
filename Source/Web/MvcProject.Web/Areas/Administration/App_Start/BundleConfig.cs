namespace MvcProject.Web.Areas.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;
    using Common.GlobalConstants;

    internal class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            // JavaScript
            bundles.Add(new ScriptBundle(Bundles.AdminAreaScriptsCustom)
                .Include(
                    "~/Areas/Administration/Scripts/Custom/datetime-handlers.js",
                    "~/Areas/Administration/Scripts/Custom/error-handler.js",
                    "~/Areas/Administration/Scripts/Custom/grid-details-helpers.js",
                    "~/Areas/Administration/Scripts/Custom/product-images-upload.js",
                    "~/Areas/Administration/Scripts/Custom/product-main-image-dropdown.js"
                    ));

            // CSS
            bundles.Add(new StyleBundle(Bundles.AdminAreaStylesCustomCss)
                .Include(
                    "~/Areas/Administration/Content/Custom/custom-popup-editor.css",
                    "~/Areas/Administration/Content/Custom/details-grid.css",
                    "~/Areas/Administration/Content/Custom/domains-list.css",
                    "~/Areas/Administration/Content/Custom/main-grid.css"
                    ));
        }
    }
}
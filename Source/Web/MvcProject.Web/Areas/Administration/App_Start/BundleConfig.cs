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
                    //"~/Scripts/Custom/datetime-handler.js",
                    //"~/Scripts/Custom/error-handler.js",
                    //"~/Scripts/Custom/grid-details-helpers.js"
                    "~/Areas/Administration/Scripts/Custom/*.js"
                    ));

            // CSS
            bundles.Add(new StyleBundle(Bundles.AdminAreaStylesCustomCss)
                .Include(
                    //"~/Content/Custom/Admin/custom-popup-editor.css",
                    //"~/Content/Custom/Admin/details-grid.css",
                    //"~/Content/Custom/Admin/domains-list.css",
                    //"~/Content/Custom/Admin/main-grid.css",
                    "~/Areas/Administration/Content/Custom/*.css"
                    ));
        }
    }
}
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
            bundles.Add(new ScriptBundle(Bundles.ScriptsCustom)
                .Include(
                    //"~/Scripts/Custom/datetime-handler.js",
                    //"~/Scripts/Custom/error-handler.js",
                    //"~/Scripts/Custom/grid-details-helpers.js"
                    "~/Areas/Administration/Scripts/Custom/*.js"
                    ));

            // CSS
            bundles.Add(new StyleBundle(Bundles.StylesCustomCss)
                //.Include("~/Content/Custom/Admin/custom-popup-editor.css", new CssRewriteUrlTransform())
                //.Include("~/Content/Custom/Admin/details-grid.css", new CssRewriteUrlTransform())
                //.Include("~/Content/Custom/Admin/domains-list.css", new CssRewriteUrlTransform())
                //.Include("~/Content/Custom/Admin/main-grid.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Administration/Content/Custom/*.css", new CssRewriteUrlTransform())
                );
        }
    }
}
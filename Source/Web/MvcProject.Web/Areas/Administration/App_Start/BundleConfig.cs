namespace MvcProject.Web.Areas.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;
    using Common.GlobalConstants;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // JavaScript
            bundles.Add(new ScriptBundle(Bundles.ScriptsCustom)
                .Include("~/Areas/Administration/Scripts/Custom/datetime-handlers.js")
                .Include("~/Areas/Administration/Scripts/Custom/error-handler.js")
                .Include("~/Areas/Administration/Scripts/Custom/grid-details-helpers.js")
                .Include("~/Areas/Administration/Scripts/Custom/product-images-upload.js")
                .Include("~/Areas/Administration/Scripts/Custom/product-main-image-dropdown.js")
                //.Include("~/Areas/Administration/Scripts/Custom/*.js")
                );

            // CSS
            bundles.Add(new StyleBundle(Bundles.StylesCustomCss)
                .Include("~/Areas/Administration/Content/Custom/custom-popup-editor.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Administration/Content/Custom/details-grid.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Administration/Content/Custom/domains-list.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Administration/Content/Custom/main-grid.css", new CssRewriteUrlTransform())
                //.Include("~/Areas/Administration/Content/Custom/*.css", new CssRewriteUrlTransform())
                );
        }
    }
}
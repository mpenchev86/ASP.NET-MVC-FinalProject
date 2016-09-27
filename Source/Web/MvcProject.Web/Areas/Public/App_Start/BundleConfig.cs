namespace MvcProject.Web.Areas.Public
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
            bundles.Add(new ScriptBundle(Bundles.ScriptsJQueryUI)
                .Include(
                    "~/Areas/Public/Scripts/IgniteUI/jquery-ui.min.js"
                    ));

            bundles.Add(new ScriptBundle(Bundles.ScriptsIgniteUI)
                .Include(
                    "~/Areas/Public/Scripts/IgniteUI/infragistics.core.Unicode.js",
                    "~/Areas/Public/Scripts/IgniteUI/infragistics.ui.rating.js",
                    "~/Areas/Public/Scripts/IgniteUI/infragistics.lob.js"
                    ));

            bundles.Add(new StyleBundle(Bundles.StylesIgniteUI)
                .Include("~/Areas/Public/Content/IgniteUI/structure/*.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Public/Content/IgniteUI/structure/modules/*.css", new CssRewriteUrlTransform())
                .Include("~/Areas/Public/Content/IgniteUI/themes/infragistics/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle(Bundles.ScriptsCustom)
                .Include(
                    "~/Areas/Public/Scripts/Custom/*.js"
                    ));

            // CSS
            bundles.Add(new StyleBundle(Bundles.StylesCustomCss)
                .Include(
                    //"~/Content/Custom/Public/listView.css"
                    "~/Areas/Public/Content/Custom/*.css", new CssRewriteUrlTransform()
                    ));
        }
    }
}
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

            //bundles.Add(new StyleBundle(Bundles.StylesIgniteUI)
            //    .Include("~/Areas/Public/Content/IgniteUI/Structure/*.css")
            //    .Include("~/Areas/Public/Content/IgniteUI/Structure/modules/*.css")
            //    .Include(
                    
            //        "~/Areas/Public/Content/IgniteUI/Themes/infragistics/*.css"
            //        ));

            bundles.Add(new ScriptBundle(Bundles.ScriptsCustom)
                .Include(
                    "~/Areas/Public/Scripts/Custom/bootstrap-modal-helpers.js",
                    "~/Areas/Public/Scripts/Custom/igniteui-rating-handler.js"
                    ));

            // CSS
            bundles.Add(new StyleBundle(Bundles.StylesCustomCss)
                .Include(
                    "~/Areas/Public/Content/Custom/homepage-carousel.css",
                    "~/Areas/Public/Content/Custom/listview.css",
                    "~/Areas/Public/Content/Custom/product-full-viewmodel.css"
                    ));
        }
    }
}
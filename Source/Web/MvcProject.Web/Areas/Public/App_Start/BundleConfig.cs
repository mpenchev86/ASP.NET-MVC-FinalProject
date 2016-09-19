﻿namespace MvcProject.Web.Areas.Public
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

            bundles.Add(new ScriptBundle(Bundles.PublicAreaScriptsIgniteUI)
                .Include(
                    "~/Areas/Public/Scripts/IgniteUI/infragistics.core.Unicode.js",
                    "~/Areas/Public/Scripts/IgniteUI/infragistics.ui.rating.js",
                    "~/Areas/Public/Scripts/IgniteUI/infragistics.lob.js"
                    ));

            bundles.Add(new StyleBundle(Bundles.PublicAreaStylesIgniteUI)
                .Include(
                    "~/Areas/Public/Content/IgniteUI/Structure/infragistics.css",
                    "~/Areas/Public/Content/IgniteUI/Structure/Modules/infragistics.ui.rating.css",
                    "~/Areas/Public/Content/IgniteUI/Themes/infragistics/infragistics.theme.css"
                    ));

            bundles.Add(new ScriptBundle(Bundles.PublicAreaScriptsCustom)
                .Include(
                    "~/Areas/Public/Scripts/Custom/bootstrap-modal-helpers.js",
                    "~/Areas/Public/Scripts/Custom/igniteui-rating-handler.js"
                    ));

            // CSS
            bundles.Add(new StyleBundle(Bundles.PublicAreaStylesCustomCss)
                .Include(
                    "~/Areas/Public/Content/Custom/homepage-carousel.css",
                    "~/Areas/Public/Content/Custom/listview.css",
                    "~/Areas/Public/Content/Custom/product-full-viewmodel.css"
                    ));
        }
    }
}
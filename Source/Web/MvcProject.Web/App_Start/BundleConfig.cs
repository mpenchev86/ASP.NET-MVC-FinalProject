namespace MvcProject.Web
{
    using System.Globalization;
    using System.Web;
    using System.Web.Optimization;

    using GlobalConstants;

    public class BundleConfig
    {
        // Improve, if possible
        public static string UiCulture = CultureInfo.CurrentUICulture.ToString();

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptsJQuery)
                .Include(
                    "~/Scripts/jquery-2.2.1.min.js"
                    ));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptsJQueryValidate)
                .Include(
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptsBootstrap)
                .Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptsKendo)
                .Include(
                    "~/Scripts/Kendo/jquery.min.js",
                    "~/Scripts/Kendo/kendo.all.min.js",
                    "~/Scripts/Kendo/kendo.aspnetmvc.min.js",
                    "~/Scripts/Kendo/cultures/kendo.culture." + UiCulture + ".min.js"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptsSignalR)
                .Include(
                    "~/Scripts/jquery.signalR-2.2.0.min.js",
                    "~/signalr/hubs"));

            // Custom*
            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptsCustom)
                .Include(
                    "~/Scripts/Custom/id-collection-helpers.js",
                    "~/Scripts/Custom/template-loader.js",
                    "~/Scripts/Custom/error-handler.js"
                ));

            bundles.Add(new StyleBundle(GlobalConstants.Bundles.StylesContentCss)
                .Include(
                    "~/Content/bootstrap.journal.css",
                    "~/Content/Site.css"));

            bundles.Add(new StyleBundle(GlobalConstants.Bundles.StylesContentKendoCss)
                .Include("~/Content/Kendo/kendo.common.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Kendo/kendo.common-bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Kendo/kendo.bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Kendo/kendo.default.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Kendo/kendo.silver.min.css", new CssRewriteUrlTransform())
                );

            bundles.Add(new StyleBundle(GlobalConstants.Bundles.StylesContentCustomCss)
                .Include(
                    "~/Content/Custom/custom-popup-editor.css",
                    "~/Content/Custom/listView.css",
                    "~/Content/Custom/Admin/domains-list.css"
                ));

            bundles.IgnoreList.Clear();

            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);

            //BundleTable.EnableOptimizations = true;

            // Optimization in both debug and release
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}

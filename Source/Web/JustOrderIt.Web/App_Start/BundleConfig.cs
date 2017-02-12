namespace JustOrderIt.Web
{
    using System.Globalization;
    using System.Web;
    using System.Web.Optimization;

    using JustOrderIt.Common.GlobalConstants;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles, string userCulture)
        {
            // JavaScript
            bundles.Add(new ScriptBundle(Bundles.ScriptsJQuery)
                .Include("~/Scripts/jquery-2.2.1.min.js"));

            bundles.Add(new ScriptBundle(Bundles.ScriptsJQueryUnobtrusive)
                .Include("~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle(Bundles.ScriptsJQueryValidate)
                .Include("~/Scripts/jquery.validate.min.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle(Bundles.ScriptsBootstrap)
                .Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle(Bundles.ScriptsKendo)
                .Include("~/Scripts/Kendo/kendo.all.min.js")
                .Include("~/Scripts/Kendo/kendo.aspnetmvc.min.js")
                .Include("~/Scripts/Kendo/cultures/kendo.culture." + userCulture + ".min.js"));

            // bundles.Add(new ScriptBundle(Bundles.ScriptsCustom)
            //    .Include(""));

            // CSS
            bundles.Add(new StyleBundle(Bundles.StylesBootStrap)
                .Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap.journal.css", new CssRewriteUrlTransform())
                .Include("~/Content/Site.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle(Bundles.StylesKendoCss)
                .Include("~/Content/Kendo/kendo.common.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Kendo/kendo.common-bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Kendo/kendo.bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.IgnoreList.Clear();

            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            bundles.IgnoreList.Ignore("*.unobtrusive-ajax.min.js", OptimizationMode.WhenDisabled);

            // bundles.Add(new StyleBundle(Bundles.StylesCustomCss)
            //    .Include("", new CssRewriteUrlTransform()));

            // Optimization in both debug and release
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}

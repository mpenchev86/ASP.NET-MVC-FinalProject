namespace MvcProject.Web
{
    using System.Web;
    using System.Web.Optimization;

    using GlobalConstants;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptJQuery).Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptKendo).Include(
                        "~/Scripts/Kendo/kendo.all.min.js",
                        "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptJQueryVal).Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptBootstrap).Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle(GlobalConstants.Bundles.StyleContentCss).Include(
                        "~/Content/bootstrap.journal.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle(GlobalConstants.Bundles.StyleContentKendoCss).Include(
                        "~/Content/Kendo/kendo.bootstrap.min.css",
                        "~/Content/Kendo/kendo.common.min.css",
                        "~/Content/Kendo/kendo.metro.min.css"));

            // Optimization in both debug and release
            BundleTable.EnableOptimizations = true;
        }
    }
}

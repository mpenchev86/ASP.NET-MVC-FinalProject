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

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptJQueryValidate).Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptBootstrap).Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptKendo).Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/Kendo/kendo.all.min.js",
                        //"~/Scripts/Kendo/kendo.web.min.js",
                        //"~/Scripts/Kendo/kendo.binder.min.js",
                        //"~/Scripts/Kendo/kendo.validator.min.js",
                        "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptSignalR).Include(
                        "~/Scripts/jquery.signalR-2.2.0.min.js",
                        "~/signalr/hubs"));

            bundles.Add(new StyleBundle(GlobalConstants.Bundles.StyleContentCss).Include(
                        "~/Content/bootstrap.journal.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle(GlobalConstants.Bundles.StyleContentKendoCss).Include(
                        "~/Content/Kendo/kendo.common.min.css",
                        "~/Content/kendo/kendo.common-bootstrap.min.css",
                        "~/Content/kendo/kendo.bootstrap.min.css",
                        "~/Content/Kendo/kendo.default.min.css",
                        "~/Content/Kendo/kendo.metro.min.css"));

            bundles.IgnoreList.Clear();

            BundleTable.EnableOptimizations = true;

//            // Optimization in both debug and release
//#if DEBUG
//            BundleTable.EnableOptimizations = false;
//#else
//            BundleTable.EnableOptimizations = true;
//#endif
        }
    }
}

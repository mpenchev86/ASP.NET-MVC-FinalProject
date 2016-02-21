namespace MvcProject.Web
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptJQuery).Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptJQueryVal).Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptModernizr).Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle(GlobalConstants.Bundles.ScriptBootstrap).Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle(GlobalConstants.Bundles.StyleContentCss).Include(
                      "~/Content/bootstrap.journal.css",
                      "~/Content/site.css"));

            // Optimization in both debug and release
            BundleTable.EnableOptimizations = true;
        }
    }
}

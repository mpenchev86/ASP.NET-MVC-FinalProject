namespace MvcProject.Web.Areas.Public
{
    using System.Web.Mvc;
    using System.Web.Optimization;

    public class PublicAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Public";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            context.MapRoute(
                name: "Public_default",
                url: "Public/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
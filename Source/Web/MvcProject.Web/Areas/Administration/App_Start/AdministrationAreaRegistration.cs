namespace MvcProject.Web.Areas.Administration
{
    using MvcProject.Common.GlobalConstants;
    using System.Web.Mvc;
    using System.Web.Optimization;

    public class AdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return Areas.AdministrationAreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            context.MapRoute(
                name: Areas.AdministrationAreaName + "_default",
                url: Areas.AdministrationAreaName + "/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
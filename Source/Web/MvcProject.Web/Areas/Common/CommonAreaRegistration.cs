namespace MvcProject.Web.Areas.Common
{
    using System.Web.Mvc;

    public class CommonAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Common";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Common_default",
                url: "Common/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
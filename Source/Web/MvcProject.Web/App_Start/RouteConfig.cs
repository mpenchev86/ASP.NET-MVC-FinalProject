namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Default",
               "{controller}/{action}/{id}",
               new { controller = "Account", action = "LogIn", id = UrlParameter.Optional },
               null,
               new string[] { "MvcProject.Web.Controllers" });
        }
    }
}

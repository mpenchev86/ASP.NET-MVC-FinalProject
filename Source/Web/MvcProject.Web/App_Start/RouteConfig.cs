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
               "Common/{controller}/{action}/{id}",
               new { controller = "Account", action = "Login", id = UrlParameter.Optional });

            //routes.MapRoute(
            //   "Default",
            //   "Public/{controller}/{action}/{id}",
            //   new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //   //,new string[]
            //   //{
            //   //     //"MvcProject.Web.Areas.Admin.Controllers",
            //   //     "MvcProject.Web.Areas.Public.Controllers",
            //   //     //"MvcProject.Web.Areas.Common.Controllers",
            //   //     //"MvcProject.Web.Areas.TestModule.Controllers",
            //   //}
            //   );

            //routes.MapRoute(
            //   "TestModule_Default",
            //   "TestModule/{controller}/{action}/{id}",
            //   new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //   //, new string[]
            //   //{
            //   //     //"MvcProject.Web.Areas.Admin.Controllers",
            //   //     "MvcProject.Web.Areas.TestModule.Controllers",
            //   //     //"MvcProject.Web.Areas.Common.Controllers",
            //   //     //"MvcProject.Web.Areas.TestModule.Controllers",
            //   //}
            //   );
        }
    }
}

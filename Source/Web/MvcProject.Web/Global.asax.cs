namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using AutoMapper;
    using Infrastructure.Mapping;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEnginesConfig.RegisterEngines(ViewEngines.Engines);

            // If problematic, create instance of AutoMapperConfig and use its Execute()
            AutoMapperConfig.Create(Assembly.GetExecutingAssembly()).Execute();
        }
    }
}

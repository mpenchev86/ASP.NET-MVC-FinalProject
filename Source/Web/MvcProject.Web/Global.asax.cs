namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Areas.Public.Controllers;
    using Data.DbAccessConfig.Contexts;
    using Data.DbAccessConfig.Migrations;
    using Infrastructure.Mapping;

#pragma warning disable SA1649 // File name must match first type name
    public class MvcApplication : HttpApplication
#pragma warning restore SA1649 // File name must match first type name
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MvcProjectDbContext, Configuration>());
            ViewEnginesConfig.RegisterEngines(ViewEngines.Engines);
            AutofacConfig.RegisterAutofac();

            AutoMapperInit.Initialize(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(BasePublicController)));
        }
    }
}

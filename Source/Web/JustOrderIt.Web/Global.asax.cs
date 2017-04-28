namespace JustOrderIt.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Areas.Administration.Controllers;
    using Areas.Public.Controllers;
    using Data.DbAccessConfig.Contexts;
    using Data.DbAccessConfig.Migrations;
    using Infrastructure.BackgroundWorkers;
    using Infrastructure.Mapping;
    using Services.Data;

#pragma warning disable SA1649 // File name must match first type name
    public class MvcApplication : HttpApplication
#pragma warning restore SA1649 // File name must match first type name
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles, CultureInfo.CurrentUICulture.ToString() ?? "en-US");

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<JustOrderItDbContext, Configuration>());
            Database.SetInitializer<HangfireDbContext>(null);
            ViewEnginesConfig.RegisterEngines(ViewEngines.Engines);
            AutofacConfig.RegisterAutofac();

            AutoMapperInit.Initialize(
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(BasePublicController)),
                Assembly.GetAssembly(typeof(BaseAdminController)),
                Assembly.GetAssembly(typeof(IBaseDataService)));

            HangfireBootstrapper.Instance.Start(new JustOrderItDbContext());
        }

        protected void Application_BeginRequest()
        {
            #if !DEBUG
            // SECURE: Ensure any request is returned over SSL/TLS in production
            if (!this.Request.IsLocal && !this.Context.Request.IsSecureConnection)
            {
                var redirect = this.Context.Request.Url.ToString().ToLower(CultureInfo.CurrentCulture).Replace("http:", "https:");
                this.Response.Redirect(redirect);
            }
            #endif
        }

        protected void Application_End(object sender, EventArgs e)
        {
            HangfireBootstrapper.Instance.Stop();
        }
    }
}

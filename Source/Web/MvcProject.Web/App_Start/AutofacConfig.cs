namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Compilation;
    using System.Web.Mvc;
    using Areas.Administration.Controllers;
    using Areas.Public.Controllers;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Data.DbAccessConfig.Contexts;
    using Data.DbAccessConfig.IdentityStores;
    using Data.DbAccessConfig.Repositories;
    using Data.Models;
    using Hangfire;
    using Infrastructure.Sanitizer;
    using Infrastructure.ViewEngines;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using MvcProject.Common.GlobalConstants;
    using Services.Data;
    using Services.Identity;
    using Services.Web;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            // If AutofacConfig's assembly doesn't reference the specific controllers assemblies, use these:
            // builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // builder.RegisterControllers(typeof(BasePublicController).Assembly);
            // builder.RegisterControllers(typeof(BaseAdminController).Assembly);
            // ...
            builder.RegisterControllers(BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray());

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Integration with Hangfire
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
        }

        public static void RegisterServices(ContainerBuilder builder)
        {
            // Application Db Context
            builder
                .Register(x => new MvcProjectDbContext())
                .As<DbContext>()
                .InstancePerLifetimeScope()
                //.InstancePerBackgroundJob()
                ;

            // Repositories
            builder
                .RegisterGeneric(typeof(EfIntPKDeletableRepository<>))
                .As(typeof(IIntPKDeletableRepository<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(EfStringPKDeletableRepository<>))
                .As(typeof(IStringPKDeletableRepository<>))
                .InstancePerLifetimeScope();

            builder
               .RegisterGeneric(typeof(EfIntPKRepository<>))
               .As(typeof(IIntPKRepository<>))
               .InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(EfStringPKRepository<>))
                .As(typeof(IStringPKRepository<>))
                .InstancePerLifetimeScope();

            // Application Services
            var dataServicesAssembly = Assembly.Load(Assemblies.DataServicesAssemblyName);
            builder
                .RegisterAssemblyTypes(dataServicesAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var webServicesAssembly = Assembly.Load(Assemblies.WebServicesAssemblyName);
            builder
                .RegisterAssemblyTypes(webServicesAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var logicServicesAssembly = Assembly.Load(Assemblies.LogicServicesAssemblyName);
            builder
                .RegisterAssemblyTypes(logicServicesAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // ASP.NET Identity Managers
            builder
                .Register(x => HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>())
                .As<UserManager<ApplicationUser, string>>()
                .InstancePerLifetimeScope()
                ;

            builder
                .Register(x => HttpContext.Current.Request.GetOwinContext().Get<ApplicationRoleManager>())
                .As<RoleManager<ApplicationRole, string>>()
                .InstancePerLifetimeScope()
                ;

            builder
                .Register(x => HttpContext.Current.Request.GetOwinContext().Get<ApplicationSignInManager>())
                .As<SignInManager<ApplicationUser, string>>()
                .InstancePerLifetimeScope()
                ;

            // Infrastructure
            var infrastructureAssembly = Assembly.Load(Assemblies.InfrastructureAssemblyName);
            builder
                .RegisterAssemblyTypes(infrastructureAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //// View Engines
            // builder
            //    .RegisterType(typeof(CustomViewLocationRazorViewEngine))
            //    .As(typeof(IViewEngine))
            //    .InstancePerRequest();
        }
    }
}
namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;

    using Areas.Common.Controllers;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Data.DbAccessConfig.Contexts;
    using Data.DbAccessConfig.IdentityStores;
    using Data.DbAccessConfig.Repositories;
    using Data.Models;
    using Infrastructure.Sanitizer;
    using Infrastructure.ViewEngines;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using MvcProject.GlobalConstants;
    using Services.Data;
    using Services.Web;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

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
        }

        public static void RegisterServices(ContainerBuilder builder)
        {
            // Application Context
            builder
                .Register(x => new MvcProjectDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            // ASP.NET Identity
            builder
                .Register(x => HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>())
                .As<UserManager<ApplicationUser, string>>()
                .InstancePerRequest()
                ;

            builder
                .Register(x => HttpContext.Current.Request.GetOwinContext().Get<ApplicationRoleManager>())
                .As<RoleManager<ApplicationRole, string>>()
                .InstancePerRequest()
                ;

            builder
                .Register(x => HttpContext.Current.Request.GetOwinContext().Get<ApplicationSignInManager>())
                .As<SignInManager<ApplicationUser, string>>()
                .InstancePerRequest()
                ;

            // Repositories
            builder
                .RegisterGeneric(typeof(EfIntPKDeletableRepository<>))
                .As(typeof(IIntPKDeletableRepository<>))
                .InstancePerRequest();

            builder
                .RegisterGeneric(typeof(EfStringPKDeletableRepository<>))
                .As(typeof(IStringPKDeletableRepository<>))
                .InstancePerRequest();

            builder
               .RegisterGeneric(typeof(EfIntPKRepository<>))
               .As(typeof(IIntPKRepository<>))
               .InstancePerRequest();

            builder
                .RegisterGeneric(typeof(EfStringPKRepository<>))
                .As(typeof(IStringPKRepository<>))
                .InstancePerRequest();

            // Application Services
            var dataServicesAssembly = Assembly.Load(Assemblies.DataServicesAssemblyName);
            builder
                .RegisterAssemblyTypes(dataServicesAssembly)
                .AsImplementedInterfaces()
                .InstancePerRequest();

            var webServicesAssembly = Assembly.Load(Assemblies.WebServicesAssemblyName);
            builder
                .RegisterAssemblyTypes(webServicesAssembly)
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Infrastructure
            var infrastructureAssembly = Assembly.Load(Assemblies.InfrastructureAssemblyName);
            builder
                .RegisterAssemblyTypes(infrastructureAssembly)
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Controllers
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>()
                .PropertiesAutowired();
        }
    }
}
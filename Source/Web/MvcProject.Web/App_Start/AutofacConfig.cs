namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
<<<<<<< HEAD

    using Areas.Common.Controllers;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Common.Constants;
    using Data.DbAccessConfig;
=======
    using Areas.Administration.Controllers;
    using Areas.Public.Controllers;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Data.DbAccessConfig.Contexts;
    using Data.DbAccessConfig.IdentityStores;
>>>>>>> master
    using Data.DbAccessConfig.Repositories;
    using Data.Models;
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
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterControllers(typeof(BasePublicController).Assembly);
            builder.RegisterControllers(typeof(BaseAdminController).Assembly);
            //builder.RegisterControllers(typeof(BaseCommonController).Assembly);

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

<<<<<<< HEAD
=======
            // Application Services
>>>>>>> master
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

<<<<<<< HEAD
=======
            // Infrastructure
>>>>>>> master
            var infrastructureAssembly = Assembly.Load(Assemblies.InfrastructureAssemblyName);
            builder
                .RegisterAssemblyTypes(infrastructureAssembly)
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Controllers
            //builder
            //    .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .AssignableTo<BaseCommonController>()
            //    .PropertiesAutowired();

            //var testModuleAssembly = Assembly.Load(Assemblies.TestModuleAssemblyName);
            //builder
            //    .RegisterAssemblyTypes(testModuleAssembly)
            //    .AssignableTo<BaseTestController>()
            //    .PropertiesAutowired();

            builder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BasePublicController)))
                .AssignableTo<BasePublicController>()
                .PropertiesAutowired();

            //builder
            //    .RegisterType(typeof(TestModule.Controllers.HomeController))
            //    .As(typeof(BaseController))
            //    .InstancePerRequest();
        }
    }
}
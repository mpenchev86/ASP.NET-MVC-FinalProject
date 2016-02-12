namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Data.DbAccessConfig;
    using Data.DbAccessConfig.Repositories;
    using Data.Models;
    using Infrastructure.ViewEngines;
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
            builder
                .Register(x => new MvcProjectDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            //builder
            //    .RegisterType(typeof(MapperConfiguration))
            //    .As(typeof(IMapperConfiguration))
            //    .InstancePerRequest();

            // builder.RegisterType(typeof(DeletableEntityRepository<SampleProduct>)).As(typeof(IRepository<SampleProduct>)).InstancePerRequest();
            // builder.RegisterGeneric(typeof(DeletableEntityRepository<>)).As(typeof(IDeletableEntityRepository<>)).InstancePerRequest();
            builder
                .RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerRequest();

            builder
                .Register(x => new SampleService())
                .As<ISampleService>()
                .InstancePerRequest();
        }
    }
}
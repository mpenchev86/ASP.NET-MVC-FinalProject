namespace JustOrderIt.Web.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Crawlers;
    using Profiles;

    public class AutoMapperConfig
    {
        public static MapperConfiguration Configuration { get; private set; }

        public void Execute(params Assembly[] assemblies)
        {
            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                if (assembly.IsFullyTrusted)
                {
                    types.AddRange(assembly.GetExportedTypes());
                }
            }

            // As per v5. recommendations, separate mapping profiles are configured
            Action<IMapperConfigurationExpression> configExpression = cfg =>
                {
                    cfg.AddProfile(new StandardMappingsProfile(types, cfg));
                    cfg.AddProfile(new ReverseMappingsProfile(types, cfg));
                    cfg.AddProfile(new CustomMappingsProfile(types, cfg));
                };

            Configuration = new MapperConfiguration(configExpression);
        }

        //private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        //{
        //    var maps = from t in types
        //               from i in t.GetInterfaces()
        //               where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
        //                     !t.IsAbstract &&
        //                     !t.IsInterface
        //               select new
        //               {
        //                   Source = i.GetGenericArguments()[0],
        //                   Destination = t
        //               };

        //    foreach (var map in maps)
        //    {
        //        mapperConfiguration.CreateMap(map.Source, map.Destination);
        //    }
        //}

        //private static void LoadReverseMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        //{
        //    var maps = from t in types
        //               from i in t.GetInterfaces()
        //               where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
        //                     !t.IsAbstract &&
        //                     !t.IsInterface
        //               select new
        //               {
        //                   Destination = i.GetGenericArguments()[0],
        //                   Source = t
        //               };

        //    foreach (var map in maps)
        //    {
        //        mapperConfiguration.CreateMap(map.Source, map.Destination);
        //    }
        //}

        //private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        //{
        //    var maps = from t in types
        //               from i in t.GetInterfaces()
        //               where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
        //                     !t.IsAbstract &&
        //                     !t.IsInterface
        //               select (IHaveCustomMappings)Activator.CreateInstance(t);

        //    foreach (var map in maps)
        //    {
        //        map.CreateMappings(mapperConfiguration);
        //    }
        //}
    }
}
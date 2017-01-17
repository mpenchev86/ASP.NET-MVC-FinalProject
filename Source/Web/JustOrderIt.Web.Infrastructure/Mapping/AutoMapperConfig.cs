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
    }
}
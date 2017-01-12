namespace JustOrderIt.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using Infrastructure.Mapping;

    public class AutoMapperInit
    {
        public static void Initialize(params Assembly[] assemblies)
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(assemblies);
        }
    }
}
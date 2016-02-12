namespace MvcProject.Web
{
    using Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    public class AutoMapperInit
    {
        public static void Initialize(Assembly assembly)
        {
            var autoMapper = new AutoMapperConfig();
            autoMapper.Execute(assembly);
        }
    }
}
﻿namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using Infrastructure.Mapping;

    public class AutoMapperInit
    {
        public static void Initialize(Assembly assembly)
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(assembly);
        }
    }
}
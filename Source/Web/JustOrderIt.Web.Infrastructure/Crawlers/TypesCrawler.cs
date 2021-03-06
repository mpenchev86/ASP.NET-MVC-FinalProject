﻿namespace JustOrderIt.Web.Infrastructure.Crawlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class TypesCrawler : ITypesCrawler
    {
        public IEnumerable<Type> GetAdministerableTypes(Assembly assembly)
        {
            var types = assembly.GetExportedTypes() /*.Where(t => !t.IsInterface && typeof(IAdministerable).IsAssignableFrom(t))*/;

            return types;
        }
    }
}

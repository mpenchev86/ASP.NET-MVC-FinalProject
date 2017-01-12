namespace JustOrderIt.Web.Infrastructure.Crawlers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ITypesCrawler
    {
        IEnumerable<Type> GetAdministerableTypes(Assembly assembly);
    }
}

namespace MvcProject.Web.Infrastructure.Crawlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Data.Models.EntityContracts;

    public class TypesCrawler : ITypesCrawler
    {
        public static IEnumerable<Type> GetAdminEntityClasses(Assembly assembly)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(t => !t.IsInterface && typeof(IAdministerable).IsAssignableFrom(t));
            return types;
        }

        // For the AutoMapperConfig
        //public static IEnumerable<object> GetMapperSourceDestinationPair(IEnumerable<Type> types, Type mappingInterface)
        //{
        //    var result = from t in types
        //                 from i in t.GetInterfaces()
        //                 where i.IsGenericType && i.GetGenericTypeDefinition() == mappingInterface &&
        //                       !t.IsAbstract &&
        //                       !t.IsInterface
        //                 select new
        //                 {
        //                     Source = i.GetGenericArguments()[0],
        //                     Destination = t
        //                 };
        //    return result;
        //}
    }
}

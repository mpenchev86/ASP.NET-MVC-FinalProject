namespace MvcProject.Web.Infrastructure.Mapping.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;

    internal class ReverseMappingsProfile : Profile
    {
        public ReverseMappingsProfile(List<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            this.LoadReverseMappings(types, mapperConfiguration);
        }

        private void LoadReverseMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                             !t.IsAbstract &&
                             !t.IsInterface
                       select new
                       {
                           Destination = i.GetGenericArguments()[0],
                           Source = t
                       };

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
            }
        }
    }
}

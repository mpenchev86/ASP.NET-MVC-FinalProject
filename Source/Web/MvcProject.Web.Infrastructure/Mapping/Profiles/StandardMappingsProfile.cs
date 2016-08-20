namespace MvcProject.Web.Infrastructure.Mapping.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;

    internal class StandardMappingsProfile : Profile
    {
        public StandardMappingsProfile(List<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            this.LoadStandardMappings(types, mapperConfiguration);
        }

        private void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                             !t.IsAbstract &&
                             !t.IsInterface
                       select new
                       {
                           Source = i.GetGenericArguments()[0],
                           Destination = t
                       };

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
            }
        }
    }
}

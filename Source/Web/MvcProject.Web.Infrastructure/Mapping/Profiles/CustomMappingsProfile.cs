namespace MvcProject.Web.Infrastructure.Mapping.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;

    internal class CustomMappingsProfile : Profile
    {
        public CustomMappingsProfile(List<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            this.LoadCustomMappings(types, mapperConfiguration);
        }

        private void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfiguration)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                             !t.IsAbstract &&
                             !t.IsInterface
                       select (IHaveCustomMappings)Activator.CreateInstance(t);

            foreach (var map in maps)
            {
                map.CreateMappings(mapperConfiguration);
            }
        }
    }
}

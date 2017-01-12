namespace JustOrderIt.Web.Infrastructure.Mapping
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;

    public class MappingService : IMappingService
    {
        private IMapper IMapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        public TDestination Map<TDestination>(object source)
        {
            return this.IMapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return this.IMapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return this.IMapper.Map(source, destination);
        }
    }
}

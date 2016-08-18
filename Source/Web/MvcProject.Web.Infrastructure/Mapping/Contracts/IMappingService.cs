namespace MvcProject.Web.Infrastructure.Mapping
{
    using AutoMapper;

    public interface IMappingService
    {
        IMapper IMapper { get; }

        T Map<T>(object source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}

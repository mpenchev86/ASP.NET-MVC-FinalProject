namespace MvcProject.Web.Infrastructure.Mapping
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(/*IMapperConfiguration*/IMapperConfigurationExpression configuration);
    }
}

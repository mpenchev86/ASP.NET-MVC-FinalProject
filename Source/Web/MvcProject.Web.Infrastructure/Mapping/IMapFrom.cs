namespace MvcProject.Web.Infrastructure.Mapping
{
    // out - "cannot convert from ProductViewModel to IMapFrom<IAdministerable>"
    public interface IMapFrom<out T>
        where T : class
    {
    }
}
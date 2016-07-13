namespace MvcProject.Web
{
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.Caching;
    using Infrastructure.Filters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AddSystemHeaderStampActionFilter());
        }
    }
}

namespace MvcProject.Web.Infrastructure.ActionFilters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    // TEST - action filter
    public class AddSystemHeaderStampActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Response.Headers.AllKeys.Any(x => x == "x-powered-by"))
            {
                filterContext.HttpContext.Response.AddHeader("x-powered-by", "My sweet project v0.1");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}

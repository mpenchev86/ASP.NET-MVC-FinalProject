namespace MvcProject.Web.Infrastructure.Filters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    // TEST - action filter
    public class LogFilter : ActionFilterAttribute
    {
        private string filePath = HttpContext.Current.Server.MapPath("~/App_Data/log.txt");

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            File.AppendAllLines(filePath, new[] { "OnActionExecuted" });
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            File.AppendAllLines(filePath, new[] { "OnActionExecuting" });
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            File.AppendAllLines(filePath, new[] { "OnResultExecuted" });
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            File.AppendAllLines(filePath, new[] { "OnResultExecuting" });
            base.OnResultExecuting(filterContext);
        }
    }
}

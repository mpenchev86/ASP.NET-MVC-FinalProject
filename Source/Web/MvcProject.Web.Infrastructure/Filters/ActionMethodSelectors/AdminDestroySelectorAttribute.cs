namespace MvcProject.Web.Infrastructure.Filters.ActionMethodSelectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class AdminDestroySelectorAttribute : ActionMethodSelectorAttribute
    {
        private readonly string firstParam;
        private readonly string secondParam;

        public AdminDestroySelectorAttribute(string firstParam, string secondParam)
        {
            this.firstParam = firstParam;
            this.secondParam = secondParam;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.RouteData.Values[this.firstParam] != null
                && controllerContext.RouteData.Values[this.secondParam] != null;
        }
    }
}

namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;

    using Common.Controllers;
    using Data.DbAccessConfig;
    using Infrastructure.Crawlers;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using MvcProject.GlobalConstants;
    using Web.Common.Constants;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        // Lists all domains for administration
        public ActionResult Index()
        {
            //var data = TypesCrawler
            //    .GetAdminEntityClasses(Assembly.Load(Assemblies.DataModelsAssemblyName))
            //    .OrderBy(t => t.Name);

            var data = typeof(IMvcProjectDbContext).GetRuntimeProperties();
            return this.View(data);
        }

        public ActionResult DomainModelRedirect(string domainModel)
        {
            return this.RedirectToAction("Index", controllerName: domainModel);
        }
    }
}
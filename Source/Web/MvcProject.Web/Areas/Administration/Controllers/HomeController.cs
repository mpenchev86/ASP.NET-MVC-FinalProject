namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System.Reflection;
    using System.Web.Mvc;

    using Data.DbAccessConfig.Contexts;
    using MvcProject.Common.GlobalConstants;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class HomeController : BaseAdminController
    {
        /// <summary>
        /// Lists all domains for administration
        /// </summary>
        /// <returns>The view with all domains for administration</returns>
        public ActionResult Index()
        {
            var data = typeof(IMvcProjectDbContext).GetRuntimeProperties();
            return this.View(data);
        }

        /// <summary>
        /// Redirects to the index page of the corresponding domain model controller
        /// </summary>
        /// <param name="domainModel">The domain model name</param>
        /// <returns>The view of the index page of the corresponding domain model controller</returns>
        public ActionResult DomainModelRedirect(string domainModel)
        {
            return this.RedirectToAction("Index", controllerName: domainModel);
        }
    }
}
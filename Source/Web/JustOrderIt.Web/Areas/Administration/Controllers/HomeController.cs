namespace JustOrderIt.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using Data.DbAccessConfig.Contexts;
    using Data.Models.Contracts;
    using JustOrderIt.Common.GlobalConstants;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class HomeController : BaseAdminController
    {
        /// <summary>
        /// Lists all domains for administration.
        /// </summary>
        /// <returns>The view with all domains for administration.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var propertyNames = typeof(IJustOrderItDbContext)
                .GetRuntimeProperties()
                .Where(p => typeof(IAdministerable).IsAssignableFrom(p.PropertyType.GetGenericArguments().Single()))
                .Select(p => p.Name)
                .OrderBy(p => p);

            return this.View(propertyNames);
        }

        /// <summary>
        /// Redirects to the index page of the corresponding domain model controller
        /// </summary>
        /// <param name="domainModel">The domain model name</param>
        /// <returns>The view of the index page of the corresponding domain model controller</returns>
        [HttpGet]
        public ActionResult DomainModelRedirect(string domainModel)
        {
            return this.RedirectToAction("Index", controllerName: domainModel);
        }
    }
}
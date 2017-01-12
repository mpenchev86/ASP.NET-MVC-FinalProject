namespace JustOrderIt.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common.GlobalConstants;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class BaseAdminController : Controller
    {
    }
}
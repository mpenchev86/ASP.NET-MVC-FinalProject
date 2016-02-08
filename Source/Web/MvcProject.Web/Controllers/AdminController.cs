namespace MvcProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        // TEST
        [AllowAnonymous]
        public ActionResult AdminPeek()
        {
            return Content("Admin Peek page you should not see:>");
            //return this.View();
        }
    }
}
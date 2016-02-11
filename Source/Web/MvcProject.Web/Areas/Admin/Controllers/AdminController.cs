namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common.Controllers;

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        public AdminController()
        {
        }

        // TEST
        [AllowAnonymous]
        public ActionResult AdminPeek()
        {
            return this.Content("Admin Peek page you should not see:>");
            //return this.View();
        }
    }
}
namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MvcProject.Web.Controllers;

    public class UsersController : BaseController
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View();
        }
    }
}
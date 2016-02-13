namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common.Controllers;
    using MvcProject.Web;

    public class UsersController : BaseController
    {
        public UsersController()
        {
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
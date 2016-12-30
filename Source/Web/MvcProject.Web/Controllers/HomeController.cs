namespace MvcProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common.GlobalConstants;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.RedirectToAction("Index", "Home", new { area = Areas.PublicAreaName });
        }
    }
}
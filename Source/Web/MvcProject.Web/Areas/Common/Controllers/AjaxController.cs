namespace MvcProject.Web.Areas.Common.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class AjaxController : Controller
    {
        public ActionResult Vote()
        {
            return this.Json(new { });
        }
    }
}
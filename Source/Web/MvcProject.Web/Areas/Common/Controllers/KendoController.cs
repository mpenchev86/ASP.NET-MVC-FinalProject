namespace MvcProject.Web.Areas.Common.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class KendoController : Controller
    {
        public ActionResult DatePicker()
        {
            return this.View();
        }
    }
}
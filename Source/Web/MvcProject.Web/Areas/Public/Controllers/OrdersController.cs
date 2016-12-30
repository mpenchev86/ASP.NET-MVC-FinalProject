namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class OrdersController : BasePublicController
    {
        public ActionResult ShoppingCart()
        {
            //var viewModel = ;
            return this.View();
        }
    }
}
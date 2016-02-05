namespace MvcProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.DbAccessConfig;
    using Data.DbAccessConfig.Repositories;
    using Data.Models;
    using MvcProject.Web.ViewModels;

    public class HomeController : Controller
    {
        private IRepository<SampleProduct> sampleProducts;
                
        public HomeController(IRepository<SampleProduct> sampleProducts)
        {
            this.sampleProducts = sampleProducts;
        }

        public ActionResult Index()
        {          
            return this.View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}
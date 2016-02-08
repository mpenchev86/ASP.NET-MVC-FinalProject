namespace MvcProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Data.DbAccessConfig.Repositories;
    using Data.Models;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<SampleProduct> sampleProducts;
                
        public HomeController(IDeletableEntityRepository<SampleProduct> sampleProducts)
        {
            this.sampleProducts = sampleProducts;
        }

        public ActionResult Index()
        {
            var sampleProducts = this.sampleProducts.All().ProjectTo<IndexSampleProductViewModel>();
            return this.View(sampleProducts);
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

        // TEST - route constraints
        public string ProductsList(int nonNullableInt, int page = 39)
        {
            string str = "";
            try
            {
                str += HttpContext.Request.Browser.Type;
            }
            catch (Exception)
            {
                str = "greshka";
            }

            return page.ToString() + "<br/>" +
                   nonNullableInt.ToString() + "<br/>" +
                   str;
        }
    }
}
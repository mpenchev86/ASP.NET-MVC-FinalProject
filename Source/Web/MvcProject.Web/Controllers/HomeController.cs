namespace MvcProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Data.DbAccessConfig;
    using Data.DbAccessConfig.Repositories;
    using Data.Models;
    using MvcProject.Web.ViewModels;
    using ViewModels.Home;

    public class HomeController : Controller
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
    }
}
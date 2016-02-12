namespace MvcProject.Web.Areas.Common.Controllers
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
    using Infrastructure.Filters;
    using ViewModels.Home;
    using Services.Web;
    [LogFilter]
    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<SampleProduct> sampleProducts;
        private ISampleService service;

        //public HomeController()
        //{
        //}

        public HomeController(IDeletableEntityRepository<SampleProduct> sampleProducts, ISampleService service)
        {
            this.sampleProducts = sampleProducts;
            this.service = service;
        }

        public ActionResult Index()
        {
            this.service.Work();
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

        #region Tests
        // TEST - route constraints
        public string ProductsList(int nonNullableInt, int page = 39)
        {
            string str = string.Empty;
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

        // TEST - FAIL - return a file result
        public FileResult GetFile(string fileName)
        {
            string fileFormat = fileName.Split(new char['.'], StringSplitOptions.RemoveEmptyEntries)[0];
            //this.Response.ClearContent();
            //this.Response.Write(filePath);
            return this.File(fileName, "application/octet-stream", "myfile." + fileFormat);

            //// file as byte array
            //var fileContent = new byte[] { (int)'0', 49, 50, 51 };
            //return this.File(fileContent, "text/plain", "mybytes.txt");
        }

        // TEST - return JS
        public JavaScriptResult ReturnJS()
        {
            return this.JavaScript("var a = 1; alert(a);");
        }

        // TEST - return Json
        public JsonResult ReturnJson()
        {
            return this.Json(new { error = "Error", code = 200 }, JsonRequestBehavior.AllowGet);
        }

        // TEST - redirect to other action
        public RedirectToRouteResult RedirectAction()
        {
            return this.RedirectToAction("About");
        }

        // TEST - redirect to a specifig URL
        public RedirectResult RedirectToURL()
        {
            return this.Redirect("https://google.com");
        }

        // TEST - attributes

        //[ActionName("AttributesTest")]
        //[NonAction]
        [AcceptVerbs("Post", "Delete", "Put")]
        //[Authorize]
        //[RequireHttps]
        //[AllowAnonymous]
        //[ChildActionOnly]
        public ActionResult Attributes()
        {
            return this.Content("Attributes");
        }

        // TEST - caching
        [OutputCache(Duration = 60 * 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult CacheMe()
        {
            var db = new MvcProjectDbContext();
            return this.View(db.Users.Count());
        }

        // TEST - choosing non-default view
        public ActionResult Pesho()
        {
            return this.View("CacheMe", null, "pepeto");
        }

        // TEST - complex object for action parameters
        public ActionResult ComplexParams(Param @param)
        {
            return this.Json(@param, JsonRequestBehavior.AllowGet);
        }

        public class Param
        {
            public int IntValue { get; set; }

            [AllowHtml]
            public string StringValue { get; set; }
        }
        #endregion
    }
}
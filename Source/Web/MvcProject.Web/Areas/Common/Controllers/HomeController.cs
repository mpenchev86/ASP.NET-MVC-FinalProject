namespace MvcProject.Web.Areas.Common.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;

    using Data.DbAccessConfig.Contexts;
    using Infrastructure.Caching;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using Services.Web;
    using ViewModels.Home;

    // [LogFilter]
    public class HomeController : BaseController
    {
        private IProductsService productsService;
        private ICategoriesService categoriesService;

        public HomeController(
            IProductsService productsService,
            ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
        }

        // [OutputCache(Duration = 30 * 60, Location = OutputCacheLocation.Server, VaryByCustom = "SomeOtherIdentifier")]
        public ActionResult Index()
        {
            return this.View();
        }

        // Cached
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.Cache.Get(
                "products",
                () => this.productsService.GetAll().To<ProductViewModel>(),
                2 * 60);

            return this.Json(viewModel.ToDataSourceResult(request));
        }

        [CommonOutputCache]
        public ActionResult FormResults()
        {
            var products = this.productsService
                .GetAll()
                .To<ProductViewModel>()
                .ToList();

            return this.View(products);
        }

        public ActionResult Search(string query)
        {
            var result = this.productsService
                .GetAll()
                .Where(x => x.Title.ToLower().Contains(query.ToLower()))
                .To<ProductViewModel>()
                .ToList();

            return this.PartialView("_ProductResult", result);
        }

        public ActionResult DescriptionById(int id)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action can be invoke only by AJAX call");
            }

            var product = this.productsService
                .GetAll()
                .FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.Content("Book not found");
            }

            return this.Content(product.ShortDescription);
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";
            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";
            return this.View();
        }

        // Async Contact
        public async Task<ActionResult> AsyncContact()
        {
            this.ViewBag.Message = "Your contact page.";
            return await Task.FromResult(this.View("Contact"));
        }

#region Tests

        // route constraints
        public string ProductsList(int nonNullableInt, int page = 39)
        {
            string str = string.Empty;
            try
            {
                str += this.HttpContext.Request.Browser.Type;
            }
            catch (Exception)
            {
                str = "greshka";
            }

            return page.ToString() + "<br/>" +
                   nonNullableInt.ToString() + "<br/>" +
                   str;
        }

        // FAIL - return a file result
        public FileResult GetFile(string fileName)
        {
            string fileFormat = fileName.Split(new char['.'], StringSplitOptions.RemoveEmptyEntries)[0];

            // this.Response.ClearContent();
            // this.Response.Write(filePath);
            return this.File(fileName, "application/octet-stream", "myfile." + fileFormat);

            //// file as byte array
            // var fileContent = new byte[] { (int)'0', 49, 50, 51 };
            // return this.File(fileContent, "text/plain", "mybytes.txt");
        }

        // return JS
        public JavaScriptResult ReturnJS()
        {
            return this.JavaScript("var a = 1; alert(a);");
        }

        // return Json
        public JsonResult ReturnJson()
        {
            return this.Json(new { error = "Error", code = 200 }, JsonRequestBehavior.AllowGet);
        }

        // redirect to other action
        public RedirectToRouteResult RedirectAction()
        {
            return this.RedirectToAction(nameof(HomeController.Index));
        }

        // redirect to a specifig URL
        public RedirectResult RedirectToURL()
        {
            return this.Redirect("https://google.com");
        }

        //// Attributes
        // [ActionName("AttributesTest")]
        // [NonAction]
        // [Authorize]
        // [RequireHttps]
        // [AllowAnonymous]
        // [ChildActionOnly]
        [AcceptVerbs("Post", "Delete", "Put")]
        public ActionResult Attributes()
        {
            return this.Content("Attributes");
        }

        // caching
        [OutputCache(Duration = 60 * 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult CacheMe()
        {
            var db = new MvcProjectDbContext();
            return this.View(db.Users.Count());
        }

        // choosing non-default view
        public ActionResult Pesho()
        {
            return this.View("CacheMe", null, "pepeto");
        }

        // complex object for action parameters
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
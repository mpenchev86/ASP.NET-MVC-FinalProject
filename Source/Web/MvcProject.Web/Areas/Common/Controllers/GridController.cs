using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using MvcProject.Web.Areas.Common.ViewModels.Kendo;

namespace MvcProject.Web.Areas.Common.Controllers
{
    public class GridController : Controller
    {
        private static IQueryable<KendoTestViewModel> data = new List<KendoTestViewModel>
        {
            new KendoTestViewModel
            {
                Id = 1,
                Name = "Pesho1",
                SomeDate = DateTime.Now.AddDays(-10)
            },
            new KendoTestViewModel
            {
                Id = 2,
                Name = "Pesho2",
                SomeDate = DateTime.Now.AddDays(-11)
            },
            new KendoTestViewModel
            {
                Id = 3,
                Name = "Pesho3",
                SomeDate = DateTime.Now.AddDays(-12)
            },
            new KendoTestViewModel
            {
                Id = 4,
                Name = "Pesho4",
                SomeDate = DateTime.Now.AddDays(-13)
            },
            new KendoTestViewModel
            {
                Id = 5,
                Name = "Pesho5",
                SomeDate = DateTime.Now.AddDays(-14)
            },
        }.AsQueryable();

        public ActionResult Index()
        {
            return View();
        }

        // This is [HttpPost] query
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = data
                .ToDataSourceResult(request/*, this.ModelState*/);

            return this.Json(result);
        }

        public ActionResult Create()
        {
            return null;
        }
    }
}
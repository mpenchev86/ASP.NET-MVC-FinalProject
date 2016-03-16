using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using MvcProject.Web.Areas.Common.ViewModels.Kendo;
using MvcProject.Web.Infrastructure.Caching;

namespace MvcProject.Web.Areas.Common.Controllers
{
    //[NoCache]
    //[OutputCache(CacheProfile = "NoCache")]
    public class GridController : Controller
    {
        public static IQueryable<KendoTestViewModel> data = new List<KendoTestViewModel>
        {
            new KendoTestViewModel
            {
                Id = 1,
                Name = "Pesho1",
                CreatedOn = DateTime.Now.AddDays(-10)
            },
            new KendoTestViewModel
            {
                Id = 2,
                Name = "Pesho2",
                CreatedOn = DateTime.Now.AddDays(-11)
            },
            new KendoTestViewModel
            {
                Id = 3,
                Name = "Pesho3",
                CreatedOn = DateTime.Now.AddDays(-12)
            },
            new KendoTestViewModel
            {
                Id = 4,
                Name = "Pesho4",
                CreatedOn = DateTime.Now.AddDays(-13)
            },
            new KendoTestViewModel
            {
                Id = 5,
                Name = "Pesho5",
                CreatedOn = DateTime.Now.AddDays(-14)
            },
        }.AsQueryable();

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = data
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, KendoTestViewModel viewModel)
        {
            if (viewModel == null || !this.ModelState.IsValid)
            {
                throw new Exception("WTF");
            }

            return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
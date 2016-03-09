﻿namespace MvcProject.Web.Areas.Common.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels.Home;
    using ViewModels.Kendo;

    public class KendoController : Controller
    {
        public ActionResult DatePicker()
        {
            var viewModel = new KendoTestViewModel
            {
                SomeDate = DateTime.Now.AddDays(10)
            };
            return this.View(viewModel);
        }

        public ActionResult PostDatePicker(KendoTestViewModel model)
        {
            return null;
        }

        public ActionResult AutoComplete()
        {
            var viewModel = new KendoTestViewModel
            {
                Countries = new List<string>
                {
                    "Bg",
                    "Zamunda",
                    "NeverMind",
                    "IsReal"
                }
            };

            return View(viewModel);
        }

        public ActionResult PostAutoComplete(KendoTestViewModel model)
        {

            return null;
        }

        public ActionResult AutocompleteData(string text)
        {
            var result = new[]
            {
                "Bg",
                "Zamunda",
                "NeverMind",
                "IsReal"
            };

            result = result
                .Where(r => r.ToLower().Contains(text.ToLower()))
                .ToArray();

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.ViewEngines;

    public class ViewEnginesConfig
    {
        public static void RegisterEngines(ViewEngineCollection viewEngines)
        {
            // Removes WebForms view engine - optimization
            viewEngines.Clear();

            // viewEngines.Add(new RazorViewEngine());
            //viewEngines.Add(new CustomViewLocationRazorViewEngine());
            viewEngines.Add(new RazorViewEngine());
        }
    }
}
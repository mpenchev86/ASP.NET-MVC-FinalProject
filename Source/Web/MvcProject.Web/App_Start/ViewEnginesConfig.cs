namespace MvcProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public static class ViewEnginesConfig
    {
        public static void RegisterEngines(ViewEngineCollection viewEngines)
        {
            // Removes WebForms view engine
            viewEngines.Clear();
            viewEngines.Add(new RazorViewEngine());
        }
    }
}
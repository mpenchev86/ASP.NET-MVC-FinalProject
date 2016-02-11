namespace MvcProject.Web.Infrastructure.ViewEngines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class CustomViewLocationRazorViewEngine : RazorViewEngine, ICustomViewEngine
    {
        public CustomViewLocationRazorViewEngine()
        {
            base.AreaViewLocationFormats = new string[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };

            base.AreaMasterLocationFormats = new string[]
            {
                "~/Areas/Common/Views/{1}/{0}.cshtml",
                "~/Areas/Common/Views/{1}/{0}.vbhtml",
                "~/Areas/Common/Views/Shared/{0}.cshtml",
                "~/Areas/Common/Views/Shared/{0}.vbhtml"
            };

            base.AreaPartialViewLocationFormats = new string[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            base.ViewLocationFormats = new string[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };
            base.MasterLocationFormats = new string[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml"
            };
            base.PartialViewLocationFormats = new string[]
            {
                "~/Areas/Common/Views/{1}/{0}.cshtml",
                "~/Areas/Common/Views/{1}/{0}.vbhtml",
                "~/Areas/Common/Views/Shared/{0}.cshtml",
                "~/Areas/Common/Views/Shared/{0}.vbhtml"
            };
        }
    }
}

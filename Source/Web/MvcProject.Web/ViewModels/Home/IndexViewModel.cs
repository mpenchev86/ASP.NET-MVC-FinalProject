﻿namespace MvcProject.Web.Areas.Common.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class IndexViewModel
    {
        public IList<ProductViewModel> Products { get; set; }
    }
}
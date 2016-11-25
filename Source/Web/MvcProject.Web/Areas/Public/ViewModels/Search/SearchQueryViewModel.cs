namespace MvcProject.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class SearchQueryViewModel
    {
        public int CategoryId { get; set; }

        public string Query { get; set; }
    }
}
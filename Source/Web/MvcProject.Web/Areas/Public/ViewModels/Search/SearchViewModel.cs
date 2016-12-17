namespace MvcProject.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using Services.Logic.ServiceModels;

    public class SearchViewModel
    {
        public int? CategoryId { get; set; }

        public string Query { get; set; }

        //public List<RefinementOption> RefinementOptions { get; set; }
    }
}
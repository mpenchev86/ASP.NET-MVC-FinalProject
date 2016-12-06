namespace MvcProject.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Services.Logic.ServiceModels;

    public class SearchByCategoryViewModel
    {
        public int? CategoryId { get; set; }

        public ICollection<RefinementOption> MyProperty { get; set; }

        public string Query { get; set; }
    }
}
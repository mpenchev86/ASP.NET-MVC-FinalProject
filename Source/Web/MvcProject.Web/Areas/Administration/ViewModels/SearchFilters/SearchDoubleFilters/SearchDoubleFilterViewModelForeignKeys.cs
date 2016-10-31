namespace MvcProject.Web.Areas.Administration.ViewModels.SearchFilters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Categories;

    public class SearchDoubleFilterViewModelForeignKeys
    {
        public IEnumerable<CategoryDetailsForSearchFilterViewModel> Categories { get; set; }
    }
}
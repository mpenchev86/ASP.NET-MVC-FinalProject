namespace JustOrderIt.Web.Areas.Administration.ViewModels.SearchFilters
{
    using System;
    using System.Collections.Generic;
    using Categories;

    public class SearchFilterViewModelForeignKeys
    {
        public IEnumerable<CategoryDetailsForSearchFilterViewModel> Categories { get; set; }
    }
}
namespace MvcProject.Web.Areas.Administration.ViewModels.Properties
{
    using System.Collections.Generic;
    using Descriptions;
    using SearchFilters;

    public class PropertyViewModelForeignKeys
    {
        public IEnumerable<DescriptionDetailsForPropertyViewModel> Descriptions { get; set; }

        public IEnumerable<SearchFilterDetailsForPropertyViewModel> SearchFilters { get; set; }
    }
}
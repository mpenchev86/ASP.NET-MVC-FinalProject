namespace JustOrderIt.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class FilteringViewModel
    {
        private ICollection<SearchFilterForCategoryViewModel> searchFilters;

        public FilteringViewModel()
        {
            this.searchFilters = new HashSet<SearchFilterForCategoryViewModel>();
        }

        public int CategoryId { get; set; }

        public string Query { get; set; }

        [UIHint("SearchFilters")]
        public ICollection<SearchFilterForCategoryViewModel> SearchFilters
        {
            get { return this.searchFilters; }
            set { this.searchFilters = value; }
        }
    }
}
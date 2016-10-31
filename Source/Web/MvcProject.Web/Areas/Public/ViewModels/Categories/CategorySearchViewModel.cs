namespace MvcProject.Web.Areas.Public.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Products;
    using Search;

    public class CategorySearchViewModel
    {
        public CategorySearchViewModel()
        {
            this.Products = new HashSet<ProductOfCategoryViewModel>();
            this.SearchFilters = new HashSet<SearchFiltersForCategoryViewModel>();
        }

        public ICollection<ProductOfCategoryViewModel> Products { get; set; }

        public ICollection<SearchFiltersForCategoryViewModel> SearchFilters { get; set; }
    }
}
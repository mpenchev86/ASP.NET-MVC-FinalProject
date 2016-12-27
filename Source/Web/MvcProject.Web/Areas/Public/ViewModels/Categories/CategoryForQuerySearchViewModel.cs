namespace MvcProject.Web.Areas.Public.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Products;
    using Search;

    public class CategoryForQuerySearchViewModel
    {
        private List<ProductForQuerySearchViewModel> products;
        private List<SearchFilterForCategoryViewModel> searchFilters;

        public CategoryForQuerySearchViewModel()
        {
            this.products = new List<ProductForQuerySearchViewModel>();
            this.searchFilters = new List<SearchFilterForCategoryViewModel>();
        }

        public List<ProductForQuerySearchViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public List<SearchFilterForCategoryViewModel> SearchFilters
        {
            get { return this.searchFilters; }
            set { this.searchFilters = value; }
        }
    }
}
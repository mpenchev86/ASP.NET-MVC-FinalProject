namespace MvcProject.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Infrastructure.Mapping;
    using Products;
    using Search;

    public class CategorySearchViewModel/* : BasePublicViewModel<int>, IMapFrom<Category>*/
    {
        private ICollection/*List*/<ProductForCategorySearchViewModel> products;
        private ICollection<SearchFilterForCategoryViewModel> searchFilters;

        public CategorySearchViewModel()
        {
            this.products = new HashSet/*List*/<ProductForCategorySearchViewModel>();
            this.searchFilters = new HashSet/*List*/<SearchFilterForCategoryViewModel>();
        }

        public int CategoryId { get; set; }

        public string Query { get; set; }

        [UIHint("PostBackProductsForCategory")]
        public ICollection/*List*/<ProductForCategorySearchViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        [UIHint("SearchFilters")]
        public ICollection<SearchFilterForCategoryViewModel> SearchFilters
        {
            get { return this.searchFilters; }
            set { this.searchFilters = value; }
        }
    }
}
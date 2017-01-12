namespace JustOrderIt.Web.Areas.Public.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;
    using Keywords;
    using Products;
    using Search;

    public class CategoryCacheViewModel : BasePublicViewModel<int>, IMapFrom<Category>
    {
        private ICollection<ProductCacheViewModel> products;
        private ICollection<SearchFilterCacheViewModel> searchFilters;
        private ICollection<KeywordCacheViewModel> keywords;

        public CategoryCacheViewModel()
        {
            this.products = new HashSet<ProductCacheViewModel>();
            this.searchFilters = new HashSet<SearchFilterCacheViewModel>();
            this.keywords = new HashSet<KeywordCacheViewModel>();
        }

        public string Name { get; set; }

        public ICollection<ProductCacheViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public ICollection<SearchFilterCacheViewModel> SearchFilters
        {
            get { return this.searchFilters; }
            set { this.searchFilters = value; }
        }

        public ICollection<KeywordCacheViewModel> Keywords
        {
            get { return this.keywords; }
            set { this.keywords = value; }
        }
    }
}
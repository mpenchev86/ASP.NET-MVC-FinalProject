namespace MvcProject.Web.Areas.Public.ViewModels.Categories
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

    public class CategorySearchViewModel : BasePublicViewModel<int>, IMapFrom<Category>
    {
        public CategorySearchViewModel()
        {
            //this.Products = new HashSet<ProductOfCategoryViewModel>();
            this.SearchFilters = new HashSet<SearchFilterForCategoryViewModel>();
        }

        public string Query { get; set; }

        //public ICollection<ProductOfCategoryViewModel> Products { get; set; }

        public ICollection<SearchFilterForCategoryViewModel> SearchFilters { get; set; }
    }
}
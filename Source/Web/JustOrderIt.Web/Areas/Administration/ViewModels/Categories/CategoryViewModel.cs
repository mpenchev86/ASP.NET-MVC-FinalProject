namespace JustOrderIt.Web.Areas.Administration.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.DataAnnotations;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Common.GlobalConstants;
    using JustOrderIt.Web.Infrastructure.Mapping;
    using Products;
    using SearchFilters;
    using Keywords;
    using Data.Models.Catalog;

    public class CategoryViewModel : BaseAdminViewModel<int>, IMapFrom<Category>
    {
        private ICollection<ProductDetailsForCategoryViewModel> products;
        private ICollection<SearchFilterDetailsForCategoryViewModel> searchFilters;
        private ICollection<KeywordDetailsForCategoryViewModel> keywords;


        public CategoryViewModel()
        {
            this.products = new HashSet<ProductDetailsForCategoryViewModel>();
            this.searchFilters = new HashSet<SearchFilterDetailsForCategoryViewModel>();
            this.keywords = new HashSet<KeywordDetailsForCategoryViewModel>();
        }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CategoryNameMaxLenght)]
        public string Name { get; set; }

        public ICollection<ProductDetailsForCategoryViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public ICollection<SearchFilterDetailsForCategoryViewModel> SearchFilters
        {
            get { return this.searchFilters; }
            set { this.searchFilters = value; }
        }

        [UIHint("MultiSelect")]
        public ICollection<KeywordDetailsForCategoryViewModel> Keywords
        {
            get { return this.keywords; }
            set { this.keywords = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }
    }
}
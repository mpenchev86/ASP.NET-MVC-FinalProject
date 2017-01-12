namespace JustOrderIt.Data.Models.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Contracts;
    using JustOrderIt.Common.GlobalConstants;
    using Search;

    /// <summary>
    /// Represents the entity for a category of products.
    /// </summary>
    public class Category : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Product> products;
        private ICollection<SearchFilter> searchFilters;
        private ICollection<Keyword> keywords;

        public Category()
        {
            this.products = new HashSet<Product>();
            this.searchFilters = new HashSet<SearchFilter>();
            this.keywords = new HashSet<Keyword>();
        }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CategoryNameMaxLenght)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the products in this category.
        /// </summary>
        /// <value>
        /// The products in this category.
        /// </value>
        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        /// <summary>
        /// Gets or sets the search filters used to filter products of this category.
        /// </summary>
        /// <value>
        /// The search filters used to filter products of this category.
        /// </value>
        public virtual ICollection<SearchFilter> SearchFilters
        {
            get { return this.searchFilters; }
            set { this.searchFilters = value; }
        }

        /// <summary>
        /// Gets or sets the search keywords associated with a category.
        /// </summary>
        /// <value>
        /// The search keywords associated with a category.
        /// </value>
        public virtual ICollection<Keyword> Keywords
        {
            get { return this.keywords; }
            set { this.keywords = value; }
        }
    }
}

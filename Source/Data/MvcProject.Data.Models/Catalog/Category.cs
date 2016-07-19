namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Contracts;
    using MvcProject.Common.GlobalConstants;

    /// <summary>
    /// Represents the entity for a category of products.
    /// </summary>
    public class Category : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Product> products;

        public Category()
        {
            this.products = new HashSet<Product>();
        }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(ValidationConstants.CategoryNameMaxLenght)]
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
    }
}

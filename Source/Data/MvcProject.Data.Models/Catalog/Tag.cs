namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Contracts;
    using MvcProject.GlobalConstants;

    /// <summary>
    /// Represents a tag entity of a product.
    /// </summary>
    public class Tag : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Product> products;

        public Tag()
        {
            this.products = new HashSet<Product>();
        }

        /// <summary>
        /// Gets or sets the tag's name.
        /// </summary>
        /// <value>
        /// The tag's name.
        /// </value>
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of products with this tag.
        /// </summary>
        /// <value>
        /// The collection of products with this tag.
        /// </value>
        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}

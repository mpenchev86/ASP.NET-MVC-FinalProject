namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Data.Models.Contracts;

    /// <summary>
    /// Represents the entity for an image of a product.
    /// </summary>
    public class Image : FileInfo
    {
        /// <summary>
        /// Gets or sets the foreign key pointing to the product.
        /// </summary>
        /// <value>
        /// The foreign key pointing to the product.
        /// </value>
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product to which the image belongs.
        /// </summary>
        /// <value>
        /// The product to which the image belongs.
        /// </value>
        [InverseProperty("Images")]
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is the main image of a product
        /// </summary>
        /// <value>
        /// A value indicating whether this is the main image of a product
        /// </value>
        public bool IsMainImage { get; set; }
    }
}
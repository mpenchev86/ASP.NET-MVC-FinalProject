namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcProject.Data.Models.EntityContracts;
    using MvcProject.GlobalConstants;

    /// <summary>
    /// Represents the entity for an image of a product.
    /// </summary>
    public class Image : BaseEntityModel<int>, IAdministerable
    {
        /// <summary>
        /// Gets or sets the original file name of the image. Does not include any path or extension.
        /// </summary>
        /// <value>
        /// The original file name of the image. Does not include any path or extension.
        /// </value>
        [Required]
        [MaxLength(ValidationConstants.ImageOriginalFileNameMaxLength)]
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets or sets the file extension of the image.
        /// </summary>
        /// <value>
        /// The file extension of the image.
        /// </value>
        [Required]
        [MaxLength(ValidationConstants.ImageFileExtensionMaxLength)]
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the full path for accessing the image file on the server.
        /// </summary>
        /// <value>
        /// The full path for accessing the image file on the server.
        /// </value>
        [Required]
        [DataType(DataType.ImageUrl)]
        [MaxLength(ValidationConstants.ImageUrlPathMaxLength)]
        public string UrlPath { get; set; }

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
    }
}
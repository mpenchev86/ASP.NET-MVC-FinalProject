namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MvcProject.Data.Models.Contracts;
    using MvcProject.GlobalConstants;

    /// <summary>
    /// Represents the entity for a user comment to a product.
    /// </summary>
    public class Comment : BaseEntityModel<int>, IAdministerable
    {
        /// <summary>
        /// Gets or sets the contents of the comment.
        /// </summary>
        /// <value>
        /// The contents of the comment.
        /// </value>
        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(ValidationConstants.CommentContentMinLength)]
        [MaxLength(ValidationConstants.CommentContentMaxLength)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the user submitting the comment.
        /// </summary>
        /// <value>
        /// The foreign key of the user submitting the comment.
        /// </value>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user submitting the comment.
        /// </summary>
        /// <value>
        /// The user submitting the comment.
        /// </value>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the product to which the comment belongs.
        /// </summary>
        /// <value>
        /// The foreign key to the product to which the comment belongs.
        /// </value>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product to which the comment belongs.
        /// </summary>
        /// <value>
        /// The product to which the comment belongs.
        /// </value>
        public virtual Product Product { get; set; }
    }
}
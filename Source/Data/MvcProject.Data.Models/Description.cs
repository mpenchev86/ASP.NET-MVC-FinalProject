namespace MvcProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EntityContracts;
    using MvcProject.GlobalConstants;

    /// <summary>
    /// Represents the entity for a description to a product.
    /// </summary>
    public class Description : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Property> properties;

        public Description()
        {
            this.properties = new HashSet<Property>();
        }

        /// <summary>
        /// Gets or sets the content of the description as a text field. Does not include the additional content
        /// in the product description properties.
        /// </summary>
        /// <value>
        /// The content of the description as a text field. Does not include the additional content
        /// in the product description properties.
        /// </value>
        [Required]
        [MaxLength(ValidationConstants.DescriptionContentMaxLength)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the properties giving additional information for the description of a product.
        /// </summary>
        /// <value>
        /// The properties giving additional information for the description of a product.
        /// </value>
        public virtual ICollection<Property> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }
    }
}
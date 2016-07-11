namespace MvcProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MvcProject.Data.Models.Contracts;

    /// <summary>
    /// Represents the property entity of a product's description.
    /// </summary>
    public class Property : BaseEntityModel<int>, IAdministerable
    {
        /// <summary>
        /// Gets or sets the name of a property.
        /// </summary>
        /// <value>
        /// The name of a property.
        /// </value>
        [Required]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of a property.
        /// </summary>
        /// <value>
        /// The value of a property.
        /// </value>
        [DataType(DataType.MultilineText)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the foreign key of the description to which the property belongs.
        /// </summary>
        /// <value>
        /// The foreign key of the description to which the property belongs.
        /// </value>
        public int DescriptionId { get; set; }

        /// <summary>
        /// Gets or sets the description to which the property belongs.
        /// </summary>
        /// <value>
        /// The description to which the property belongs.
        /// </value>
        [InverseProperty("Properties")]
        public virtual Description Description { get; set; }
    }
}
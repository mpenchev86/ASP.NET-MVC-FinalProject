namespace JustOrderIt.Data.Models.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Contracts;
    using Identity;
    using JustOrderIt.Common.GlobalConstants;

    /// <summary>
    /// Represents a vote entity of a product.
    /// </summary>
    public class Vote : BaseEntityModel<int>, IAdministerable
    {
        /// <summary>
        /// Gets or sets the value of a vote.
        /// </summary>
        /// <value>
        /// The value of a vote.
        /// </value>
        [Required]
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int VoteValue { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the product to which the vote belongs.
        /// </summary>
        /// <value>
        /// The foreign key to the product to which the vote belongs.
        /// </value>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product to which the vote belongs.
        /// </summary>
        /// <value>
        /// The product to which the vote belongs.
        /// </value>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the user who casted the vote.
        /// </summary>
        /// <value>
        /// The foreign key to the user who casted the vote.
        /// </value>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who casted the vote.
        /// </summary>
        /// <value>
        /// The user who casted the vote.
        /// </value>
        public virtual ApplicationUser User { get; set; }
    }
}

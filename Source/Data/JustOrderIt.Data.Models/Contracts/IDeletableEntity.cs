namespace JustOrderIt.Data.Models.Contracts
{
    using System;

    /// <summary>
    /// Defines the properties implemented by entities that can be marked as deleted and remain in the database.
    /// </summary>
    public interface IDeletableEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is marked as deleted
        /// </summary>
        /// <value>
        /// A value indicating whether the entity is marked as deleted
        /// </value>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was marked as deleted
        /// </summary>
        /// <value>
        /// The date and time when the entity was marked as deleted
        /// </value>
        DateTime? DeletedOn { get; set; }
    }
}

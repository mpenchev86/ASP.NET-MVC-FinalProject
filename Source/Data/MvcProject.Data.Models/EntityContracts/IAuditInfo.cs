namespace MvcProject.Data.Models.EntityContracts
{
    using System;

    /// <summary>
    /// Defines properties used for auditing of an entity.
    /// </summary>
    public interface IAuditInfo
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        /// <value>
        /// The date and time when the entity was created.
        /// </value>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        /// <value>
        /// The date and time when the entity was last modified.
        /// </value>
        DateTime? ModifiedOn { get; set; }
    }
}

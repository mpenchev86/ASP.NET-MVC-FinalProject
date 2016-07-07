namespace MvcProject.Data.Models.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Base class for application domain entities
    /// </summary>
    /// <typeparam name="TKey">The type of the entity's primary key</typeparam>
    public class BaseEntityModel<TKey> : IBaseEntityModel<TKey>, IAuditInfo, IDeletableEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the entity
        /// </summary>
        /// <value>
        /// The unique identifier of the entity
        /// </value>
        [Key]
        public TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created
        /// </summary>
        /// <value>
        /// The date and time when the entity was created
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified
        /// </summary>
        /// <value>
        /// The date and time when the entity was last modified
        /// </value>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is marked as deleted
        /// </summary>
        /// <value>
        /// A value indicating whether the entity is marked as deleted
        /// </value>
        [Index]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was marked as deleted
        /// </summary>
        /// <value>
        /// The date and time when the entity was marked as deleted
        /// </value>
        public DateTime? DeletedOn { get; set; }
    }
}

namespace JustOrderIt.Data.Models.Contracts
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base interface inherited by all entities
    /// </summary>
    /// <typeparam name="TKey">The type of the entity's primary key</typeparam>
    public interface IBaseEntityModel<TKey>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the entity
        /// </summary>
        /// <value>
        /// The unique identifier of the entity
        /// </value>
        [Key]
        TKey Id { get; set; }
    }
}

namespace JustOrderIt.Services.Data
{
    using System.Linq;
    using JustOrderIt.Data.Models.Contracts;

    /// <summary>
    /// Extends the base data service interface with functionality for IDeletable entities.
    /// </summary>
    /// <typeparam name="T">The type of data entity which the service manipulates.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public interface IDeletableEntitiesBaseService<T, TKey> : IBaseDataService<T, TKey>
        where T : class, IBaseEntityModel<TKey>
    {
        IQueryable<T> GetAllNotDeleted();

        T GetByIdFromNotDeleted(TKey id);

        T GetByEncodedIdFromNotDeleted(string id);

        void MarkAsDeleted(TKey id);

        void MarkAsDeleted(T entity);
    }
}

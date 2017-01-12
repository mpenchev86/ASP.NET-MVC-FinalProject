namespace JustOrderIt.Data.DbAccessConfig.Repositories
{
    using System.Linq;

    /// <summary>
    /// Implements repository functionality for entities implementing the IDeletableEntity interface.
    /// </summary>
    /// <typeparam name="T">The type of the entity which the repository manages.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public interface IDeletableRepository<T, TKey>
    {
        IQueryable<T> AllNotDeleted();

        void DeleteMark(T entity);

        T GetByIdFromNotDeleted(TKey id);
    }
}

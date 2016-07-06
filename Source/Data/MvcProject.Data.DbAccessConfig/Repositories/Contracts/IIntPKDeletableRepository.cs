namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using Models.EntityContracts;

    /// <summary>
    /// Implements the base repository interface and the repository functionality for entities
    /// implementing the IDeletableEntity interface with integer primary key.
    /// </summary>
    /// <typeparam name="T">The type of the entity which the repository manages.</typeparam>
    public interface IIntPKDeletableRepository<T> : IRepository<T, int>, IDeletableRepository<T, int>
        where T : class, IBaseEntityModel<int>
    {
    }
}

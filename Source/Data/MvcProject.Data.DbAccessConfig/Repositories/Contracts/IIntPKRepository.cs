namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using Models.Contracts;

    /// <summary>
    /// Implements the base repository interface with integer entity primary key.
    /// </summary>
    /// <typeparam name="T">The type of the entity which the repository manages.</typeparam>
    public interface IIntPKRepository<T> : IRepository<T, int>
        where T : class, IBaseEntityModel<int>
    {
    }
}

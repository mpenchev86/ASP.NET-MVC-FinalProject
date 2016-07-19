namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using Models.Contracts;

    /// <summary>
    /// Implements the base repository interface with string entity primary key.
    /// </summary>
    /// <typeparam name="T">The type of the entity which the repository manages.</typeparam>
    public interface IStringPKRepository<T> : IRepository<T, string>
        where T : class, IBaseEntityModel<string>
    {
    }
}

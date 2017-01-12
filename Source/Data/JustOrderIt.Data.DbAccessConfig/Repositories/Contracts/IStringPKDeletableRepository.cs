namespace JustOrderIt.Data.DbAccessConfig.Repositories
{
    using Models.Contracts;

    /// <summary>
    /// Implements the base repository interface and the repository functionality for entities implementing
    /// the IDeletableEntity interface with string primary key.
    /// </summary>
    /// <typeparam name="T">The type of the entity which the repository manages.</typeparam>
    public interface IStringPKDeletableRepository<T> : IRepository<T, string>, IDeletableRepository<T, string>
        where T : class, IBaseEntityModel<string>
    {
    }
}

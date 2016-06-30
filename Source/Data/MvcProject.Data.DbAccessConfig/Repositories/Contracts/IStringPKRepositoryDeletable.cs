namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using Models.EntityContracts;

    public interface IStringPKRepositoryDeletable<T> : IRepository<T, string>, IDeletableRepository<T, string>
        where T : class, IBaseEntityModel<string>
    {
    }
}

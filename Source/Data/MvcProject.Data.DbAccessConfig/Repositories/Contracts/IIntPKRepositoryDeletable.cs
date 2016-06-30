namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using Models.EntityContracts;

    public interface IIntPKRepositoryDeletable<T> : IRepository<T, int>, IDeletableRepository<T, int>
        where T : class, IBaseEntityModel<int>
    {
    }
}

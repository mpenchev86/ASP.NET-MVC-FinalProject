namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using Models.EntityContracts;

    public interface IIntPKRepository<T> : IRepository<T, int>
        where T : class, IBaseEntityModel<int>
    {
    }
}

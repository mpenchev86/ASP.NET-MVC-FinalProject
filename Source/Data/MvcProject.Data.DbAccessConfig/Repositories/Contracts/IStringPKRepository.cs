namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using Models.EntityContracts;

    public interface IStringPKRepository<T> : IRepository<T, string>
        where T : class, IBaseEntityModel<string>
    {
    }
}

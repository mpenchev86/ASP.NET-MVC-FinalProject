namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System.Linq;

    public interface IDeletableRepository<T, TKey>
    {
        IQueryable<T> AllNotDeleted();

        void DeleteMark(T entity);

        T GetByIdFromNotDeleted(TKey id);
    }
}

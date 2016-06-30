namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System.Linq;
    using Models.EntityContracts;

    public interface IRepository<T, TKey>
        where T : IBaseEntityModel<TKey>
    {
        IQueryable<T> All();

        T GetById(TKey id);

        void Add(T entity);

        void Update(T entity);

        void DeletePermanent(T entity);

        void DeletePermanent(TKey id);

        void Detach(T entity);

        void SaveChanges();

        void Dispose();
    }
}

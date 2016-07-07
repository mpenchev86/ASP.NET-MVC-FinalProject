namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System.Linq;
    using Models.Contracts;

    /// <summary>
    /// The repository interface containing basic CRUD functionality.
    /// </summary>
    /// <typeparam name="T">The type of the entity which the repository manages.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
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

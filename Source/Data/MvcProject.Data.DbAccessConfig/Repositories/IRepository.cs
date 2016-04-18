namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System;
    using System.Linq;
    using Models.EntityContracts;

    public interface IRepository<T> : IRepository<T, int>
        where T : IBaseEntityModel<int>
    {
    }

    public interface IRepository<T, in TKey>
        where T : IBaseEntityModel<TKey>
    {
        IQueryable<T> All();

        IQueryable<T> AllWithMarkedDeleted();

        T GetById(TKey id);

        void Add(T entity);

        void DeleteMark(T entity);

        void DeletePermanent(T entity);

        void Save();
    }
}

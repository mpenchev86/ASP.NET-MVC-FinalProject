namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System;
    using System.Linq;
    using Models.EntityContracts;

    public interface IRepository<T> : IRepository<T, int>
        where T : class, IBaseEntityModel<int>
    {
    }

    //public interface IUserRepository<T> : IRepository<T, string>
    //    where T : class, IBaseEntityModel<string>
    //{
    //}

    public interface IRepository<T, TKey>
        where T : IBaseEntityModel<TKey>
    {
        IQueryable<T> All();

        IQueryable<T> AllNotDeleted();

        T GetById(TKey id);

        T GetByIdFromAll(TKey id);

        void Add(T entity);

        void Update(T entity);

        void DeleteMark(T entity);

        void DeletePermanent(T entity);

        void DeletePermanent(int id);

        void Detach(T entity);

        void SaveChanges();

        void Dispose();
    }
}

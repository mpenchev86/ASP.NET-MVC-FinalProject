namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models.EntityContracts;

    /// <summary>
    /// Exposes base functionality for data manipulation services as an abstraction over the data access layer
    /// </summary>
    /// <typeparam name="T">The type of data entity which the service manipulates.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public interface IBaseService<T, TKey>
        where T : class, IBaseEntityModel<TKey>
    {
        IQueryable<T> GetAll();

        T GetById(TKey id);

        T GetByEncodedId(string id);

        void Insert(T entity);

        void Update(T entity);

        void DeletePermanent(TKey id);

        void DeletePermanent(T entity);
    }
}

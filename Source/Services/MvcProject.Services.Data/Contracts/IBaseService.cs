namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models.EntityContracts;

    public interface IBaseService<T, TKey>
        where T : class, IAdministerable
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

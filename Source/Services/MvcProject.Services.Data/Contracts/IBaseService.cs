namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models.EntityContracts;
    using MvcProject.Web.Infrastructure.Mapping;

    // out - "cannot convert from IProductsService to IBaseService<IAdministerable>"
    public interface IBaseService<T, TKey>
        where T : class, IAdministerable
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAllNotDeleted();

        T GetById(TKey id);

        T GetByEncodedId(string id);

        T GetByIdFromNotDeleted(TKey id);

        T GetByEncodedIdFromNotDeleted(string id);

        void Insert(T entity);

        void Update(T entity);

        void MarkAsDeleted(TKey id);

        void MarkAsDeleted(T entity);

        void DeletePermanent(TKey id);

        void DeletePermanent(T entity);
    }
}

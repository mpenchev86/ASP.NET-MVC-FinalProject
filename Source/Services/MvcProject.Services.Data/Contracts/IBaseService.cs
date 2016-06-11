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
    public interface IBaseService<T>
        where T : class, IAdministerable
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAllNotDeleted();

        T GetById(int id);

        T GetById(string id);

        T GetByIdFromNotDeleted(int id);

        T GetByIdFromNotDeleted(string id);

        void Insert(T propertyEntity);

        void Update(T propertyEntity);

        void MarkAsDeleted(int id);

        void DeletePermanent(int id);

        void DeletePermanent(T propertyEntity);
    }
}

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
    public interface IDeletableEntitiesBaseService<T, TKey> : IBaseService<T, TKey>
        where T : class, IAdministerable
    {
        IQueryable<T> GetAllNotDeleted();

        T GetByIdFromNotDeleted(TKey id);

        T GetByEncodedIdFromNotDeleted(string id);

        void MarkAsDeleted(TKey id);

        void MarkAsDeleted(T entity);
    }
}

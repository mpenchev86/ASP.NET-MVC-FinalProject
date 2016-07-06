namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models.EntityContracts;
    using MvcProject.Web.Infrastructure.Mapping;

    /// <summary>
    /// Extends the base data service with functionality for IDeletable entities.
    /// </summary>
    /// <typeparam name="T">The type of data entity which the service manipulates.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public interface IDeletableEntitiesBaseService<T, TKey> : IBaseService<T, TKey>
        where T : class, IBaseEntityModel<TKey>
    {
        IQueryable<T> GetAllNotDeleted();

        T GetByIdFromNotDeleted(TKey id);

        T GetByEncodedIdFromNotDeleted(string id);

        void MarkAsDeleted(TKey id);

        void MarkAsDeleted(T entity);
    }
}

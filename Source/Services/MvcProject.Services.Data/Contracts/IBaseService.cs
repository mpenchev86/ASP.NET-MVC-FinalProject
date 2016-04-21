namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    // out - "cannot convert from IProductsService to IBaseService<IAdministerable>"
    public interface IBaseService<out T>
        where T : class
    {
        IQueryable<T> GetAll();

        T GetById(string id);
    }
}

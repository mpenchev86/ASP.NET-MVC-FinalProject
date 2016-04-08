namespace MvcProject.Services.Data
{
    using System.Linq;
    using MvcProject.Data.Models;

    public interface IProductsService : IBaseService<Product>
    {
        IQueryable<Product> GetRandomProducts(int count);

        Product GetById(string count);
    }
}
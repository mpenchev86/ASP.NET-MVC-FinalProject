namespace MvcProject.Services.Data
{
    using System.Linq;
    using MvcProject.Data.Models;

    public interface IProductsService
    {
        IQueryable<SampleProduct> GetAllProducts();

        IQueryable<SampleProduct> GetRandomProducts(int count);
    }
}
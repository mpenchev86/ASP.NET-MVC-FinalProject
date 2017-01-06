namespace MvcProject.Services.Data
{
    using System.Linq;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.Catalog;
    using MvcProject.Web.Infrastructure.Mapping;

    /// <summary>
    /// Allows extension of the data service for Product entity
    /// </summary>
    public interface IProductsService : IDeletableEntitiesBaseService<Product, int>
    {
        //void UpdateWithConcurrencyCheck(Product entity);
    }
}
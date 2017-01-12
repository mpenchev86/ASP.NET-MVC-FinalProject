namespace JustOrderIt.Services.Data
{
    using System.Linq;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Data.Models.Catalog;
    using JustOrderIt.Web.Infrastructure.Mapping;

    /// <summary>
    /// Allows extension of the data service for Product entity
    /// </summary>
    public interface IProductsService : IDeletableEntitiesBaseService<Product, int>
    {
    }
}
namespace JustOrderIt.Services.Data
{
    using JustOrderIt.Data.Models.Catalog;

    /// <summary>
    /// Allows extension of the data service for Product entity
    /// </summary>
    public interface IProductsService : IDeletableEntitiesBaseService<Product, int>
    {
    }
}
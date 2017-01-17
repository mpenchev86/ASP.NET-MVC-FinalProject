namespace JustOrderIt.Services.Data
{
    using JustOrderIt.Data.Models.Catalog;

    /// <summary>
    /// Allows extension of the data service for Category entity
    /// </summary>
    public interface ICategoriesService : IDeletableEntitiesBaseService<Category, int>
    {
        Category GetByName(string categoryName);
    }
}

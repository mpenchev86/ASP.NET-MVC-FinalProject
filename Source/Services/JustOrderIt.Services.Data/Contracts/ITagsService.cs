namespace JustOrderIt.Services.Data
{
    using JustOrderIt.Data.Models.Catalog;

    /// <summary>
    /// Allows extension of the data service for Tag entity
    /// </summary>
    public interface ITagsService : IDeletableEntitiesBaseService<Tag, int>
    {
    }
}

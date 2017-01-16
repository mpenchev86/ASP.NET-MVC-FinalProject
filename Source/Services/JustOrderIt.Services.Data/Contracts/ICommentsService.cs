namespace JustOrderIt.Services.Data
{
    using JustOrderIt.Data.Models.Catalog;

    /// <summary>
    /// Allows extension of the data service for Comment entity
    /// </summary>
    public interface ICommentsService : IDeletableEntitiesBaseService<Comment, int>
    {
    }
}

namespace JustOrderIt.Services.Data
{
    using JustOrderIt.Data.Models.Catalog;

    /// <summary>
    /// Allows extension of the data service for the ApplicationUser entity
    /// </summary>
    public interface IVotesService : IDeletableEntitiesBaseService<Vote, int>
    {
    }
}

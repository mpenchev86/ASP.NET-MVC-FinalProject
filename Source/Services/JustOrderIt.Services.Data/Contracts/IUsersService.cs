namespace JustOrderIt.Services.Data
{
    using JustOrderIt.Data.Models.Identity;

    /// <summary>
    /// Allows extension of the data service for the ApplicationUser entity
    /// </summary>
    public interface IUsersService : IDeletableEntitiesBaseService<ApplicationUser, string>
    {
        ApplicationUser GetByUserName(string userName);
    }
}

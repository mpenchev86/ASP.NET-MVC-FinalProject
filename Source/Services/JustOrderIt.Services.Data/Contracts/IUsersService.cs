namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Data.Models.Identity;
    using JustOrderIt.Web.Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Allows extension of the data service for the ApplicationUser entity
    /// </summary>
    public interface IUsersService : IDeletableEntitiesBaseService<ApplicationUser, string>
    {
        ApplicationUser GetByUserName(string userName);
    }
}

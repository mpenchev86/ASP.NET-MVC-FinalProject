namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.Identity;
    using MvcProject.Web.Infrastructure.Mapping;

    /// <summary>
    /// Allows extension of the data service for the ApplicationUser entity
    /// </summary>
    public interface IUsersService : IDeletableEntitiesBaseService<ApplicationUser, string>
    {
        ApplicationUser GetByUserName(string userName);
    }
}

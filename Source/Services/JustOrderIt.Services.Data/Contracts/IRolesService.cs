namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Data.Models.Identity;

    /// <summary>
    /// Allows extension of the data service for ApplicationRole entity
    /// </summary>
    public interface IRolesService : IDeletableEntitiesBaseService<ApplicationRole, string>
    {
        ApplicationRole GetByName(string name);
    }
}

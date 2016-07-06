namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models;

    /// <summary>
    /// Allows extension of the data service for ApplicationRole entity
    /// </summary>
    public interface IRolesService : IDeletableEntitiesBaseService<ApplicationRole, string>
    {
        ApplicationRole GetByName(string name);
    }
}

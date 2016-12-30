namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.Catalog;
    using MvcProject.Web.Infrastructure.Mapping;

    /// <summary>
    /// Allows extension of the data service for Description entity
    /// </summary>
    public interface IDescriptionsService : IDeletableEntitiesBaseService<Description, int>
    {
    }
}

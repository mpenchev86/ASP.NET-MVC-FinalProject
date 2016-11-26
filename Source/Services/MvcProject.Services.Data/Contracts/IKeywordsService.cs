namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models;

    /// <summary>
    /// Allows extension of the data service for Keyword entity
    /// </summary>
    public interface IKeywordsService : IDeletableEntitiesBaseService<Keyword, int>
    {
    }
}

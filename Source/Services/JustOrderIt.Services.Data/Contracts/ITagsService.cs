namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Data.Models.Catalog;
    using JustOrderIt.Web.Infrastructure.Mapping;

    /// <summary>
    /// Allows extension of the data service for Tag entity
    /// </summary>
    public interface ITagsService : IDeletableEntitiesBaseService<Tag, int>
    {
    }
}

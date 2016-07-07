namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.Contracts;
    using MvcProject.Web.Infrastructure.Mapping;

    /// <summary>
    /// Allows extension of the data service for Comment entity
    /// </summary>
    public interface ICommentsService : IDeletableEntitiesBaseService<Comment, int>
    {
    }
}

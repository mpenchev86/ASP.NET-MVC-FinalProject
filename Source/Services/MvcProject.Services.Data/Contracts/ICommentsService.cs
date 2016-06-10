namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.EntityContracts;
    using MvcProject.Web.Infrastructure.Mapping;

    public interface ICommentsService : IBaseService<Comment>
    {
    }
}

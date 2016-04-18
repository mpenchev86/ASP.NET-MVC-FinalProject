namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MvcProject.Data.Models;

    public interface IUsersService : IBaseService<ApplicationUser>
    {
        ApplicationUser GetUserById(string id);

        IQueryable<string> GetUserRoles(string userId);
    }
}

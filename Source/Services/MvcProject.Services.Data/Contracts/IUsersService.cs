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
    using MvcProject.Web.Infrastructure.Mapping;

    public interface IUsersService : IBaseService<ApplicationUser, string>
    {
        IQueryable<ApplicationRole> GetAllRoles();

        IQueryable<string> GetUserRoles(string userId);

        Task<IdentityResult> RemoveFromRoles(string userId, string[] roles);

        Task<IdentityResult> RemoveFromRole(string userId, string role);

        Task<IdentityResult> AddToRoles(string userId, string[] roles);
    }
}

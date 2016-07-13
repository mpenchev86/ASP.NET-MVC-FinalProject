namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Exposes functionality for manipulation of data in the junction table for users and roles
    /// </summary>
    /// <typeparam name="T">The type representing the junction table.</typeparam>
    public interface IUserRolesService<T>
    {
        IQueryable<T> GetByUserId(string userId);

        IQueryable<T> GetByUserName(string userName);

        IQueryable<T> GetByRoleId(string roleId);

        IQueryable<T> GetByRoleName(string roleName);

        void CreateUserRole(T role);

        void RemoveUserFromRole(string userId, string role);

        void RemoveUserFromRoles(string userId, string[] roles);

        void RemoveUserFromAllRoles(string userName);
    }
}

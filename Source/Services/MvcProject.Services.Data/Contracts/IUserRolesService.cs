namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserRolesService<T>
    {
        IQueryable<T> GetByUserId(string userId);

        IQueryable<T> GetByUserName(string userName);

        IQueryable<T> GetByRoleId(string roleId);

        IQueryable<T> GetByRoleName(string roleName);

        void CreateUserRole(T role);

        //void AddUserToRoles(string userId, string[] roles);

        void RemoveUserFromRole(string userId, string role);

        void RemoveUserFromRoles(string userId, string[] roles);

        void RemoveUserFromAllRoles(string userName);
    }
}

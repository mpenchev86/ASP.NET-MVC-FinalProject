namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;

    public class UserRolesService : IUserRolesService<ApplicationUserRole>
    {
        private readonly IIntPKRepository<ApplicationUserRole> repository;

        public UserRolesService(IIntPKRepository<ApplicationUserRole> repository)
        {
            this.repository = repository;
        }

        public IQueryable<ApplicationUserRole> GetByRoleId(string roleId)
        {
            return this.repository.All().Where(r => r.RoleId == roleId);
        }

        public IQueryable<ApplicationUserRole> GetByRoleName(string roleName)
        {
            return this.repository.All().Where(r => r.RoleName == roleName);
        }

        public IQueryable<ApplicationUserRole> GetByUserId(string userId)
        {
            return this.repository.All().Where(r => r.UserId == userId);
        }

        public IQueryable<ApplicationUserRole> GetByUserName(string userName)
        {
            return this.repository.All().Where(r => r.UserName == userName);
        }

        public void AddUserToRole(ApplicationUserRole role)
        {
            this.repository.Add(role);
            this.repository.SaveChanges();
        }

        public void AddUserToRoles(string userId, string[] roles)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromRole(string userName, string roleName)
        {
            this.repository.DeletePermanent(this.GetByUserName(userName).FirstOrDefault(r => r.RoleName == roleName));
            this.repository.SaveChanges();
        }

        public void RemoveUserFromRoles(string userId, string[] roles)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromAllRoles(string userName)
        {
            foreach (var userRole in this.GetByUserName(userName))
            {
                this.repository.DeletePermanent(userRole);
            }

            this.repository.SaveChanges();
        }
    }
}

namespace MvcProject.Services.Data
{
    using System.Linq;
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

        public void CreateUserRole(ApplicationUserRole role)
        {
            this.repository.Add(role);
            this.repository.SaveChanges();
        }

        public void RemoveUserFromRole(string userName, string roleName)
        {
            this.repository.DeletePermanent(this.GetByUserName(userName).FirstOrDefault(r => r.RoleName == roleName));
            this.repository.SaveChanges();
        }

        public void RemoveUserFromRoles(string userId, string[] roles)
        {
            var userRoles = this.repository.All().Where(r => (r.UserId == userId) && roles.Contains(r.RoleName));
            foreach (var userRole in userRoles)
            {
                this.repository.DeletePermanent(userRole);
            }

            this.repository.SaveChanges();
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

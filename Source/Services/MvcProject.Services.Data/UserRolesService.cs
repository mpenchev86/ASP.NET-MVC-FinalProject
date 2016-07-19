namespace MvcProject.Services.Data
{
    using System.Linq;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;

    public class UserRolesService : IUserRolesService<ApplicationUserRole>
    {
        private readonly IIntPKRepository<ApplicationUserRole> userRolesRepository;

        public UserRolesService(IIntPKRepository<ApplicationUserRole> repository)
        {
            this.userRolesRepository = repository;
        }

        public IQueryable<ApplicationUserRole> GetByRoleId(string roleId)
        {
            return this.userRolesRepository.All().Where(r => r.RoleId == roleId);
        }

        public IQueryable<ApplicationUserRole> GetByRoleName(string roleName)
        {
            return this.userRolesRepository.All().Where(r => r.RoleName == roleName);
        }

        public IQueryable<ApplicationUserRole> GetByUserId(string userId)
        {
            return this.userRolesRepository.All().Where(r => r.UserId == userId);
        }

        public IQueryable<ApplicationUserRole> GetByUserName(string userName)
        {
            return this.userRolesRepository.All().Where(r => r.UserName == userName);
        }

        public void CreateUserRole(ApplicationUserRole role)
        {
            this.userRolesRepository.Add(role);
            this.userRolesRepository.SaveChanges();
        }

        public void RemoveUserFromRole(string userName, string roleName)
        {
            this.userRolesRepository.DeletePermanent(this.GetByUserName(userName).FirstOrDefault(r => r.RoleName == roleName));
            this.userRolesRepository.SaveChanges();
        }

        public void RemoveUserFromRoles(string userId, string[] roles)
        {
            var userRoles = this.userRolesRepository.All().Where(r => (r.UserId == userId) && roles.Contains(r.RoleName));
            foreach (var userRole in userRoles)
            {
                this.userRolesRepository.DeletePermanent(userRole);
            }

            this.userRolesRepository.SaveChanges();
        }

        public void RemoveUserFromAllRoles(string userName)
        {
            foreach (var userRole in this.GetByUserName(userName))
            {
                this.userRolesRepository.DeletePermanent(userRole);
            }

            this.userRolesRepository.SaveChanges();
        }
    }
}

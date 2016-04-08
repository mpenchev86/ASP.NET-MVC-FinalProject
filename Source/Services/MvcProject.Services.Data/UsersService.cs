namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using System.Reflection;

    public class UsersService : IUsersService
    {
        //private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<ApplicationUser> users;

        public UsersService(IRepository<ApplicationUser> users)
        {
            this.users = users;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return this.users.All();
        }

        public ApplicationUser GetById(string id)
        {
            var result = this.users
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return result;
        }
    }
}

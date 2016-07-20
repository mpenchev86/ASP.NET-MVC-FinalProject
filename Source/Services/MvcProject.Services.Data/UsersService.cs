namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using MvcProject.Data.DbAccessConfig;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using Web;

    public class UsersService : BaseDataService<ApplicationUser, string, IStringPKDeletableRepository<ApplicationUser>>, IUsersService
    {
        private readonly IStringPKDeletableRepository<ApplicationUser> usersRepository;
        private IIdentifierProvider idProvider;
        private UserManager<ApplicationUser, string> userManager;

        public UsersService(IStringPKDeletableRepository<ApplicationUser> users, IIdentifierProvider idProvider, UserManager<ApplicationUser, string> userManager)
            : base(users, idProvider)
        {
            this.usersRepository = users;
            this.userManager = userManager;
            this.idProvider = idProvider;
        }

        public ApplicationUser GetByUserName(string userName)
        {
            return this.userManager.FindByName(userName);
        }

        public override ApplicationUser GetByEncodedId(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var user = this.usersRepository.GetById(decodedId);
            return user;
        }

        public override ApplicationUser GetByEncodedIdFromNotDeleted(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var user = this.usersRepository.GetByIdFromNotDeleted(decodedId);
            return user;
        }
    }
}

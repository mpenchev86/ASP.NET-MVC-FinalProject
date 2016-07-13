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
        private readonly IStringPKDeletableRepository<ApplicationUser> users;
        private IIdentifierProvider idProvider;

        public UsersService(IStringPKDeletableRepository<ApplicationUser> users, IIdentifierProvider idProvider)
            : base(users, idProvider)
        {
            this.users = users;
            this.idProvider = idProvider;
        }

        public override ApplicationUser GetByEncodedId(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var user = this.users.GetById(decodedId);
            return user;
        }

        public override ApplicationUser GetByEncodedIdFromNotDeleted(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var user = this.users.GetByIdFromNotDeleted(decodedId);
            return user;
        }
    }
}

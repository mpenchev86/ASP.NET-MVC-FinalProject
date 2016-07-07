namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using Web;

    public class RolesService : BaseDataService<ApplicationRole, string, IStringPKDeletableRepository<ApplicationRole>>, IRolesService
    {
        private readonly IStringPKDeletableRepository<ApplicationRole> roles;
        private readonly RoleManager<ApplicationRole, string> roleManager;
        private IIdentifierProvider idProvider;

        public RolesService(
            IStringPKDeletableRepository<ApplicationRole> roles,
            RoleManager<ApplicationRole, string> roleManager,
            IIdentifierProvider idProvider)
            : base(roles, idProvider)
        {
            this.roles = roles;
            this.roleManager = roleManager;
            this.idProvider = idProvider;
        }

        public ApplicationRole GetByName(string name)
        {
            return this.roleManager.FindByName(name);
        }

        public override ApplicationRole GetByEncodedId(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var role = this.roles.GetById(decodedId);
            return role;
        }

        public override ApplicationRole GetByEncodedIdFromNotDeleted(string id)
        {
            var decodedId = this.idProvider.DecodeIdToString(id);
            var role = this.roles.GetByIdFromNotDeleted(decodedId);
            return role;
        }
    }
}

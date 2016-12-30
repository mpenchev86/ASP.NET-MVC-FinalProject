namespace MvcProject.Services.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Data.DbAccessConfig.Contexts;
    using Data.DbAccessConfig.IdentityStores;
    using Data.Models;
    using Data.Models.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    public class ApplicationRoleManager : RoleManager<ApplicationRole, string>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store)
            : base(store)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context.Get<MvcProjectDbContext>()));
            return roleManager;
        }
    }
}
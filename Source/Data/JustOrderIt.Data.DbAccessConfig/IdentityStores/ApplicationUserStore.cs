namespace JustOrderIt.Data.DbAccessConfig.IdentityStores
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.Identity;

    /// <summary>
    /// Inherits and extends the EntityFramework based user store implementation that supports
    /// IUserStore, IUserLoginStore, IUserClaimStore and IUserRoleStore
    /// </summary>
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, ApplicationUserRole, IdentityUserClaim>
    {
        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }
    }
}

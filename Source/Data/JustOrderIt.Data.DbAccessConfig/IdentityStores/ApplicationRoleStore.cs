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
    /// Inherits and extends the Entity Framework implementation of a basic role management.
    /// </summary>
    public class ApplicationRoleStore : RoleStore<ApplicationRole, string, ApplicationUserRole>
    {
        public ApplicationRoleStore(DbContext context)
            : base(context)
        {
        }
    }
}

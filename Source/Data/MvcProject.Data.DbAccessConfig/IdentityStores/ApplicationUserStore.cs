namespace MvcProject.Data.DbAccessConfig
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, ApplicationUserRole, IdentityUserClaim>, IDisposable
    {
        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }
    }
}

namespace MvcProject.Web.Infrastructure.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] rolesArray)
            : base()
        {
            this.Roles = string.Join(",", rolesArray);
        }
    }
}

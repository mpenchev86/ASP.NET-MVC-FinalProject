namespace JustOrderIt.Web.Infrastructure.Filters
{
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

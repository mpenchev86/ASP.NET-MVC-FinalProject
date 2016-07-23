namespace MvcProject.Web.Infrastructure.ActionResults
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;

    public class ChallengeResult : HttpUnauthorizedResult
    {
        private string xsrfKey;

        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null, null, null)
        {
        }

        public ChallengeResult(string provider, string redirectUri, string userId, IOwinContext owinContext, string xsrfKey)
        {
            this.LoginProvider = provider;
            this.RedirectUri = redirectUri;
            this.UserId = userId;
            this.xsrfKey = xsrfKey;
            this.OwinContext = owinContext;
        }

        public string LoginProvider { get; set; }

        public string RedirectUri { get; set; }

        public string UserId { get; set; }

        public IOwinContext OwinContext { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            // Suppresses FormsAuthentication on redirect. Used for external authentication.
            context.RequestContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;

            var properties = new AuthenticationProperties { RedirectUri = this.RedirectUri };
            if (this.UserId != null)
            {
                properties.Dictionary[this.xsrfKey] = this.UserId;
            }

            this.OwinContext.Authentication.Challenge(properties, this.LoginProvider);
        }
    }
}

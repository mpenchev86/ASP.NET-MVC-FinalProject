using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcProject.Web.Startup))]
namespace MvcProject.Web
{
    using System.Globalization;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.GlobalConstants;
    using Hangfire;
    using Infrastructure.Extensions;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Used to prevent CultureInfo leaking in other AppDomains(signalR)
            // https://github.com/SignalR/SignalR/issues/3414
            app.SanitizeThreadCulture();

            this.ConfigureAuth(app);
        }
    }
}

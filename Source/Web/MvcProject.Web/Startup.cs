using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcProject.Web.Startup))]
namespace MvcProject.Web
{
    using System.Globalization;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure.Extensions;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Used to prevent blocking all threads (signalR)
            app.SanitizeThreadCulture();
            this.ConfigureAuth(app);

            // app.MapSignalR();
        }
    }
}

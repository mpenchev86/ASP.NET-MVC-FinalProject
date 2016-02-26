using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcProject.Web.Startup))]

namespace MvcProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

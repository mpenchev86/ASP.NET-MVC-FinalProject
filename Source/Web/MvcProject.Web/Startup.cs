using System.Threading.Tasks;
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

            // app.MapSignalR();

            //// This is a temp fix of app.MapSignalR() memory leak or sth ()
            //var task = Task.Run(() => app.MapSignalR());
            //task.Wait(300);

            //// try again if it fails just to be sure ;)
            //if (task.IsCanceled)
            //{
            //    Task.Run(() => app.MapSignalR()).Wait(300);
            //}
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyCollege.WebApp.Startup))]

namespace MyCollege.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles(); 
            app.MapSignalR();
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Weather.MVC.Startup))]
namespace Weather.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

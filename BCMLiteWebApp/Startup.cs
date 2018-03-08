using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BCMLiteWebApp.Startup))]
namespace BCMLiteWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

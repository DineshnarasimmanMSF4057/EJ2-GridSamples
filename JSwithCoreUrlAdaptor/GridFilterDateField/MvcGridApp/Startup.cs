using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcGridApp.Startup))]

namespace MvcGridApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

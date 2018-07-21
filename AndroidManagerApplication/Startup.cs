using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AndroidManagerApplication.Startup))]
namespace AndroidManagerApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

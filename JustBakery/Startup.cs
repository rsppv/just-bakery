using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JustBakery.Startup))]
namespace JustBakery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

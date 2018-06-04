using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BANGTANS.Startup))]
namespace BANGTANS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

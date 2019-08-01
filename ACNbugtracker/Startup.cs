using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ACNbugtracker.Startup))]
namespace ACNbugtracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektGrupowy.Startup))]
namespace ProjektGrupowy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

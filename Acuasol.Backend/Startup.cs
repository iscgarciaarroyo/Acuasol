using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Acuasol.Backend.Startup))]
namespace Acuasol.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Clinic_Registry.Startup))]
namespace Clinic_Registry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

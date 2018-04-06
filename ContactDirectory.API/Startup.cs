using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ContactDirectory.API.Startup))]

namespace ContactDirectory.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}
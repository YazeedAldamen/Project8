using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(universityERP.Startup))]
namespace universityERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(USBA.Web.Startup))]
namespace USBA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

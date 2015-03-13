using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetBundling.TestWebsite.Startup))]
namespace AspNetBundling.TestWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

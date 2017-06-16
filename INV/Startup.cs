using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(INV.Startup))]
namespace INV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

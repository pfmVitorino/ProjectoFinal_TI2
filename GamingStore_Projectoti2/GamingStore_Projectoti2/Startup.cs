using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GamingStore_Projectoti2.Startup))]
namespace GamingStore_Projectoti2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

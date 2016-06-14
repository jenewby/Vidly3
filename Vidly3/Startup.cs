using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vidly3.Startup))]
namespace Vidly3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

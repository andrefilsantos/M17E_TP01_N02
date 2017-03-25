using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(M17E_TP01_N02.Startup))]
namespace M17E_TP01_N02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}

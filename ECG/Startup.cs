using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECG.Startup))]
namespace ECG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

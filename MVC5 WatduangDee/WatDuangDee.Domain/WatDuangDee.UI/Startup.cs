using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WatDuangDee.UI.Startup))]
namespace WatDuangDee.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

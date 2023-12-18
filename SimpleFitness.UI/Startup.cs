using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleFitness.UI.Startup))]
namespace SimpleFitness.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

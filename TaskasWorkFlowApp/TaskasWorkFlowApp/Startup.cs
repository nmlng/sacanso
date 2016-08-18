using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskasWorkFlowApp.Startup))]
namespace TaskasWorkFlowApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

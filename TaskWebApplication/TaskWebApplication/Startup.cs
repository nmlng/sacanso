using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskWebApplication.Startup))]
namespace TaskWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskWeb.Startup))]
namespace TaskWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

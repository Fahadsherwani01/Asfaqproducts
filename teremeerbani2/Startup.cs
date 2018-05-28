using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(teremeerbani2.Startup))]
namespace teremeerbani2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

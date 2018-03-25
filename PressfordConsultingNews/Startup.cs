using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PressfordConsultingNews.Startup))]
namespace PressfordConsultingNews
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

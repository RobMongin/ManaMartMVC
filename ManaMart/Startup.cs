using Microsoft.Owin;
using Owin;
using ManaMart.Services;

[assembly: OwinStartupAttribute(typeof(ManaMart.Startup))]
namespace ManaMart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var service = new RoleService();
            service.CreateAdmin();
            service.MakeMyUserAdmin();
        }
    }
}

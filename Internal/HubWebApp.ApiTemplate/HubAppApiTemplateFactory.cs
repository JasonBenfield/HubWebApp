using HubWebApp.Api;
using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;

namespace HubWebApp.ApiTemplate
{
    public sealed class HubAppApiTemplateFactory
    {
        public AppApiTemplate Create()
        {
            var services = new ServiceCollection();
            var sp = services.BuildServiceProvider();
            var api = new HubAppApi
            (
                new AppApiSuperUser(),
                "Current",
                new AuthGroupFactory(sp),
                new UserAdminGroupFactory(sp)
            );
            return api.Template();
        }
    }
}

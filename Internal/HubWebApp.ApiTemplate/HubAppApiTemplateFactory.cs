using HubWebApp.Api;
using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.ApiTemplate
{
    public sealed class HubAppApiTemplateFactory : IAppApiTemplateFactory
    {
        public AppApiTemplate Create()
        {
            var services = new ServiceCollection();
            var sp = services.BuildServiceProvider();
            var api = new HubAppApi
            (
                new AppApiSuperUser(),
                AppVersionKey.Current,
                new UserAdminGroupFactory(sp)
            );
            return api.Template();
        }
    }
}

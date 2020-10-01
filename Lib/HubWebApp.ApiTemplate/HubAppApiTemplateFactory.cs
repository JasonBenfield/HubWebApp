using HubWebApp.Api;
using XTI_App.Api;

namespace HubWebApp.ApiTemplate
{
    public sealed class HubAppApiTemplateFactory
    {
        public AppApiTemplate Create()
        {
            var api = new HubAppApi
            (
                new AppApiSuperUser(),
                new AuthGroupFactoryForTemplate(),
                new UserAdminFactoryForTemplate()
            );
            return api.Template();
        }
    }
}

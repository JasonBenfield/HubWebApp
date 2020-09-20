using HubWebApp.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiTemplate
{
    public sealed class HubAppApiTemplateFactory
    {
        public AppApiTemplate Create()
        {
            var api = new HubAppApi
            (
                new SuperUser(),
                new AuthGroupFactoryForTemplate(),
                new UserAdminFactoryForTemplate()
            );
            return api.Template();
        }
    }
}

using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_HubAppApi.AppRegistration;

namespace XTI_HubAppApi
{
    public sealed class HubAppSetup : IAppSetup
    {
        private readonly HubAppApi hubApi;
        private readonly AppApiFactory apiFactory;

        public HubAppSetup(HubAppApi hubApi, AppApiFactory apiFactory)
        {
            this.hubApi = hubApi;
            this.apiFactory = apiFactory;
        }

        public async Task Run(AppVersionKey versionKey)
        {
            var versions = await hubApi.AppRegistration.GetVersions.Invoke(HubInfo.AppKey);
            var template = apiFactory.CreateTemplate();
            var request = new RegisterAppRequest
            {
                AppTemplate = template.ToModel(),
                VersionKey = versionKey,
                Versions = versions
            };
            await hubApi.AppRegistration.RegisterApp.Invoke(request);
        }
    }
}

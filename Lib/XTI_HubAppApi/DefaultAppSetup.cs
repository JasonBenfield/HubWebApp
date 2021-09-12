using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_HubAppApi.AppRegistration;

namespace XTI_HubAppApi
{
    public sealed class DefaultAppSetup : IAppSetup
    {
        private readonly HubAppApi hubApi;
        private readonly AppApiFactory apiFactory;
        private readonly PersistedVersions persistedVersions;

        public DefaultAppSetup(HubAppApi hubApi, AppApiFactory apiFactory, PersistedVersions persistedVersions)
        {
            this.hubApi = hubApi;
            this.apiFactory = apiFactory;
            this.persistedVersions = persistedVersions;
        }

        public async Task Run(AppVersionKey versionKey)
        {
            var versions = await persistedVersions.Versions();
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

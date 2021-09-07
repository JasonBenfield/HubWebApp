using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HubClientAppContext : ISourceAppContext
    {
        private readonly HubAppClient hubClient;
        private readonly AppKey appKey;
        private readonly AppVersionKey versionKey;
        private string modKey;

        public HubClientAppContext(HubAppClient hubClient, AppKey appKey, AppVersionKey versionKey)
        {
            this.hubClient = hubClient;
            this.appKey = appKey;
            this.versionKey = versionKey;
        }

        public async Task<IApp> App()
        {
            var modKey = await GetModifierKey();
            var app = await hubClient.App.GetApp(modKey);
            return new HubClientApp(hubClient, this, app);
        }

        public async Task<IAppVersion> Version()
        {
            var modKey = await GetModifierKey();
            var version = await hubClient.Version.GetVersion(modKey, versionKey);
            return new HubClientVersion(hubClient, this, version);
        }

        public async Task<string> GetModifierKey()
        {
            if (string.IsNullOrWhiteSpace(modKey))
            {
                var model = new GetAppModifierKeyRequest
                {
                    AppName = appKey.Name.Value,
                    AppType = appKey.Type.Value
                };
                modKey = await hubClient.Apps.GetAppModifierKey(model);
            }
            return modKey;
        }

    }
}

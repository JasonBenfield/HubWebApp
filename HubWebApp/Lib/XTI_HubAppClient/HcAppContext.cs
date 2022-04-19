using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HcAppContext : ISourceAppContext
    {
        private readonly HubAppClient hubClient;
        private readonly AppKey appKey;
        private readonly AppVersionKey versionKey;
        private string modKey = "";

        public HcAppContext(HubAppClient hubClient, AppKey appKey, AppVersionKey versionKey)
        {
            this.hubClient = hubClient;
            this.appKey = appKey;
            this.versionKey = versionKey;
        }

        public async Task<IApp> App()
        {
            var modKey = await GetModifierKey();
            var app = await hubClient.App.GetApp(modKey);
            return new HcApp(hubClient, this, app);
        }

        public async Task<IAppVersion> Version()
        {
            var modKey = await GetModifierKey();
            var version = await hubClient.Version.GetVersion(modKey, versionKey.Value);
            return new HcVersion(hubClient, this, version);
        }

        public async Task<string> GetModifierKey()
        {
            if (string.IsNullOrWhiteSpace(modKey))
            {
                var appWithModifier = await hubClient.Apps.GetAppByAppKey
                (
                    new GetAppByAppKeyRequest { AppKey = appKey }
                );
                modKey = appWithModifier.ModKey;
            }
            return modKey;
        }

        public async Task<ModifierKey> ModKeyInHubApps(IApp app)
        {
            var appWithModifier = await hubClient.Apps.GetAppById
            (
                new GetAppByIDRequest { AppID = app.ID.Value }
            );
            return new ModifierKey(appWithModifier.ModKey);
        }
    }
}

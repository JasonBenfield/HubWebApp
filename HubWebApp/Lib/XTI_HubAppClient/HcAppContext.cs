using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HcAppContext : ISourceAppContext
    {
        private readonly HubAppClient hubClient;
        private readonly AppVersionKey versionKey;

        public HcAppContext(HubAppClient hubClient, AppVersionKey versionKey)
        {
            this.hubClient = hubClient;
            this.versionKey = versionKey;
        }

        public Task<AppContextModel> App() => 
            hubClient.System.GetAppContext
            (
                new GetAppContextRequest
                {
                    VersionKey = versionKey.Value
                }
            );
    }
}

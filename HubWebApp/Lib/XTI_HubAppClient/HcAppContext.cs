using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HcAppContext : ISourceAppContext
    {
        private readonly HubAppClient hubClient;

        public HcAppContext(HubAppClient hubClient)
        {
            this.hubClient = hubClient;
        }

        public Task<AppContextModel> App() => hubClient.System.GetAppContext();
    }
}

using XTI_App.Api;

namespace XTI_HubAppClient
{
    public sealed class HcAppContext : ISourceAppContext
    {
        private readonly HubAppClient hubClient;
        private readonly InstallationIDAccessor installationIDAccessor;

        public HcAppContext(HubAppClient hubClient, InstallationIDAccessor installationIDAccessor)
        {
            this.hubClient = hubClient;
            this.installationIDAccessor = installationIDAccessor;
        }

        public async Task<AppContextModel> App()
        {
            var installationID = await installationIDAccessor.Value();
            var appContextModel = await hubClient.System.GetAppContext
            (
                new GetAppContextRequest
                {
                    InstallationID = installationID
                }
            );
            return appContextModel;
        }

        public Task<ModifierModel> Modifier(ModifierCategoryModel category, ModifierKey modKey) =>
            hubClient.System.GetModifier
            (
                new GetModifierRequest(category.ID, modKey)
            );
    }
}

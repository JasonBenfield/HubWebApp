using XTI_App.Api;

namespace XTI_HubAppClient;

public sealed class HcAppContext : ISourceAppContext
{
    private readonly HubAppClient hubClient;
    private readonly InstallationIDAccessor installationIDAccessor;

    public HcAppContext(HubAppClient hubClient, InstallationIDAccessor installationIDAccessor)
    {
        this.hubClient = hubClient;
        this.installationIDAccessor = installationIDAccessor;
    }

    public async Task<AppContextModel> App(CancellationToken ct)
    {
        var installationID = await installationIDAccessor.Value();
        var appContextModel = await hubClient.System.GetAppContext
        (
            new GetAppContextRequest
            {
                InstallationID = installationID
            },
            ct
        );
        return appContextModel;
    }

    public Task<ModifierModel> Modifier(ModifierCategoryModel category, ModifierKey modKey, CancellationToken ct) =>
        hubClient.System.GetModifier
        (
            new GetModifierRequest(category.ID, modKey),
            ct
        );
}

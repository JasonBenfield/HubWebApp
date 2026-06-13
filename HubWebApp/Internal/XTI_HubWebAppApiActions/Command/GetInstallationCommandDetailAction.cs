namespace XTI_HubWebAppApiActions.Command;

public sealed class GetInstallationCommandDetailAction : AppAction<AppCommandIDRequest, AppInstallCommandDetailModel>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public GetInstallationCommandDetailAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public Task<AppInstallCommandDetailModel> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken) =>
        hubService.GetInstallCommandDetail
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.CommandID, 
            stoppingToken
        );
}

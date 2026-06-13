namespace XTI_HubWebAppApiActions.Installation;

public sealed class AddDeleteCommandAction : AppAction<InstallationIDRequest, AppDeleteCommandDetailModel>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public AddDeleteCommandAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public Task<AppDeleteCommandDetailModel> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken) =>
        hubService.AddDeleteCommand
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.InstallationID, 
            stoppingToken
        );
}
namespace XTI_HubWebAppApiActions.Command;

public sealed class BeginInstallationAction : AppAction<BeginInstallationRequest, InstallationModel>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public BeginInstallationAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public Task<InstallationModel> Execute(BeginInstallationRequest requestData, CancellationToken stoppingToken) =>
        hubService.BeginInstallation
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.CommandID, 
            requestData.IsCurrent, 
            stoppingToken
        );
}
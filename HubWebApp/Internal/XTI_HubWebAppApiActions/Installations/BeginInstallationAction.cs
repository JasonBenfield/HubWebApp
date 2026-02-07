namespace XTI_HubWebAppApiActions.Installations;

public sealed class BeginInstallationAction : AppAction<BeginInstallationRequest, InstallationModel>
{
    private readonly IHubService hubService;

    public BeginInstallationAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<InstallationModel> Execute(BeginInstallationRequest requestData, CancellationToken stoppingToken) =>
        hubService.BeginInstallation
        (
            requestData.CommandID, 
            requestData.IsCurrent, 
            stoppingToken
        );
}
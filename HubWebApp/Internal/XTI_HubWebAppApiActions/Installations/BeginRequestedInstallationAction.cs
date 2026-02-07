namespace XTI_HubWebAppApiActions.Installations;

public sealed class BeginRequestedInstallationAction : AppAction<AppCommandIDRequest, AppCommandModel>
{
    private readonly IHubService hubService;

    public BeginRequestedInstallationAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppCommandModel> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken) =>
        hubService.BeginInstallCommand(requestData.CommandID, stoppingToken);
}

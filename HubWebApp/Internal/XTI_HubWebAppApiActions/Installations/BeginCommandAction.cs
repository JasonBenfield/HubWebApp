namespace XTI_HubWebAppApiActions.Installations;

public sealed class BeginCommandAction : AppAction<AppCommandIDRequest, AppCommandModel>
{
    private readonly IHubService hubService;

    public BeginCommandAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppCommandModel> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken) =>
        hubService.BeginCommand(requestData.CommandID, stoppingToken);
}

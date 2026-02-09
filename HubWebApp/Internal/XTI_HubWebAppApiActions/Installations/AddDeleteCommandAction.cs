namespace XTI_HubWebAppApiActions.Installations;

public sealed class AddDeleteCommandAction : AppAction<InstallationIDRequest, AppDeleteCommandDetailModel>
{
    private readonly IHubService hubService;

    public AddDeleteCommandAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppDeleteCommandDetailModel> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken) =>
        hubService.AddDeleteCommand(requestData.InstallationID, stoppingToken);
}
namespace XTI_HubWebAppApiActions.Installations;

public sealed class GetInstallationCommandDetailAction : AppAction<AppCommandIDRequest, AppInstallCommandDetailModel>
{
    private readonly IHubService hubService;

    public GetInstallationCommandDetailAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppInstallCommandDetailModel> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken) =>
        hubService.GetInstallCommandDetail(requestData.CommandID, stoppingToken);
}

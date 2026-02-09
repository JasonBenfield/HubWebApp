namespace XTI_HubWebAppApiActions.Installations;

public sealed class GetDeleteCommandDetailAction : AppAction<AppCommandIDRequest, AppDeleteCommandDetailModel>
{
    private readonly IHubService hubService;

    public GetDeleteCommandDetailAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppDeleteCommandDetailModel> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken) =>
        hubService.GetDeleteCommandDetail(requestData.CommandID, stoppingToken);
}

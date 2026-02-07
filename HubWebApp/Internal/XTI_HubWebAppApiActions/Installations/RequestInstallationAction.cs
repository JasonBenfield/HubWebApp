namespace XTI_HubWebAppApiActions.Installations;

public sealed class RequestInstallationAction : AppAction<AddAppInstallCommandRequest, AppInstallCommandDetailModel>
{
    private readonly IHubService hubService;

    public RequestInstallationAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppInstallCommandDetailModel> Execute(AddAppInstallCommandRequest requestData, CancellationToken stoppingToken) =>
        hubService.AddInstallCommand
        (
            requestData,
            stoppingToken
        );
}
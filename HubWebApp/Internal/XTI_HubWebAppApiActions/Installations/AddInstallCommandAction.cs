namespace XTI_HubWebAppApiActions.Installations;

public sealed class AddInstallCommandAction : AppAction<AddInstallCommandRequest, AppInstallCommandDetailModel>
{
    private readonly IHubService hubService;

    public AddInstallCommandAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppInstallCommandDetailModel> Execute(AddInstallCommandRequest requestData, CancellationToken stoppingToken) =>
        hubService.AddInstallCommand
        (
            requestData,
            stoppingToken
        );
}
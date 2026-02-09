namespace XTI_HubWebAppApiActions.Installations;

public sealed class DeletedAction : AppAction<InstallationIDRequest, EmptyActionResult>
{
    private readonly IHubService hubService;

    public DeletedAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public async Task<EmptyActionResult> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken)
    {
        await hubService.Deleted(requestData.InstallationID, stoppingToken);
        return new EmptyActionResult();
    }
}
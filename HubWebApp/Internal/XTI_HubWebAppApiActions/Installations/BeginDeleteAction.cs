namespace XTI_HubWebAppApiActions.Installations;

public sealed class BeginDeleteAction : AppAction<InstallationIDRequest, EmptyActionResult>
{
    private readonly IHubService hubService;

    public BeginDeleteAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public async Task<EmptyActionResult> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken)
    {
        await hubService.BeginDelete(requestData.InstallationID, stoppingToken);
        return new EmptyActionResult();
    }
}
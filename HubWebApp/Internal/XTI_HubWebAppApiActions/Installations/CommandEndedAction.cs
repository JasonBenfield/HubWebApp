namespace XTI_HubWebAppApiActions.Installations;

public sealed class CommandEndedAction : AppAction<AppCommandIDRequest, EmptyActionResult>
{
    private readonly IHubService hubService;

    public CommandEndedAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public async Task<EmptyActionResult> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken)
    {
        await hubService.CommandEnded(requestData.CommandID, stoppingToken);
        return new EmptyActionResult();
    }
}

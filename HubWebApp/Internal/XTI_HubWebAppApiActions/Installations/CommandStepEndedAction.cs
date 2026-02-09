namespace XTI_HubWebAppApiActions.Installations;

public sealed class CommandStepEndedAction : AppAction<AppCommandStepEndedRequest, EmptyRequest>
{
    private readonly IHubService hubService;

    public CommandStepEndedAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public async Task<EmptyRequest> Execute(AppCommandStepEndedRequest requestData, CancellationToken stoppingToken)
    {
        await hubService.CommandStepEnded(requestData.StepID, requestData.ErrorMessage, stoppingToken);
        return new EmptyRequest();
    }
}

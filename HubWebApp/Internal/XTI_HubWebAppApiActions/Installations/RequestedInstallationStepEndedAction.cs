namespace XTI_HubWebAppApiActions.Installations;

public sealed class RequestedInstallationStepEndedAction : AppAction<AppCommandStepEndedRequest, EmptyRequest>
{
    private readonly IHubService hubService;

    public RequestedInstallationStepEndedAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public async Task<EmptyRequest> Execute(AppCommandStepEndedRequest requestData, CancellationToken stoppingToken)
    {
        await hubService.CommandStepEnded(requestData.StepID, requestData.ErrorMessage, stoppingToken);
        return new EmptyRequest();
    }
}

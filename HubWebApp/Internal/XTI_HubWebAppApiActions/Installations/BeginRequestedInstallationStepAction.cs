namespace XTI_HubWebAppApiActions.Installations;

public sealed class BeginRequestedInstallationStepAction : AppAction<BeginAppCommandStepRequest, AppCommandStepModel>
{
    private readonly IHubService hubService;

    public BeginRequestedInstallationStepAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppCommandStepModel> Execute(BeginAppCommandStepRequest requestData, CancellationToken stoppingToken) =>
        hubService.BeginCommandStep(requestData.CommandID, requestData.Activity, stoppingToken);
}

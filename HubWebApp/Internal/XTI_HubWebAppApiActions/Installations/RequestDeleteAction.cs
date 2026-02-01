namespace XTI_HubWebAppApiActions.Installations;

public sealed class RequestDeleteAction : AppAction<GetInstallationRequest, EmptyActionResult>
{
    private readonly EfHubDB hubFactory;

    public RequestDeleteAction(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(GetInstallationRequest model, CancellationToken stoppingToken)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(model.InstallationID, stoppingToken);
        await installation.RequestDelete(stoppingToken);
        return new EmptyActionResult();
    }
}

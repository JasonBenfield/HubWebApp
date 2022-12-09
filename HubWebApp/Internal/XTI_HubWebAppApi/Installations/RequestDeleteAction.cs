namespace XTI_HubWebAppApi.Installations;

internal sealed class RequestDeleteAction : AppAction<GetInstallationRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;

    public RequestDeleteAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(GetInstallationRequest model, CancellationToken stoppingToken)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(model.InstallationID);
        await installation.RequestDelete();
        return new EmptyActionResult();
    }
}

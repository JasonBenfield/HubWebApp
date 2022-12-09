namespace XTI_HubWebAppApi.Installations;

public sealed class BeginDeleteAction : AppAction<GetInstallationRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;

    public BeginDeleteAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(GetInstallationRequest model, CancellationToken stoppingToken)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(model.InstallationID);
        await installation.BeginDelete();
        return new EmptyActionResult();
    }
}
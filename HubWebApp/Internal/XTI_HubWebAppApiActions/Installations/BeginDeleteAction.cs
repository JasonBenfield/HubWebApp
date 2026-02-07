namespace XTI_HubWebAppApiActions.Installations;

public sealed class BeginDeleteAction : AppAction<InstallationIDRequest, EmptyActionResult>
{
    private readonly EfHubDB hubFactory;

    public BeginDeleteAction(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(InstallationIDRequest model, CancellationToken stoppingToken)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(model.InstallationID, stoppingToken);
        await installation.BeginDelete(stoppingToken);
        return new EmptyActionResult();
    }
}
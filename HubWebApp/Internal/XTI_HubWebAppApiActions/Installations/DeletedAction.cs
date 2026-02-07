namespace XTI_HubWebAppApiActions.Installations;

public sealed class DeletedAction : AppAction<InstallationIDRequest, EmptyActionResult>
{
    private readonly EfHubDB hubFactory;

    public DeletedAction(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(InstallationIDRequest model, CancellationToken stoppingToken)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(model.InstallationID, stoppingToken);
        await installation.Deleted(stoppingToken);
        return new EmptyActionResult();
    }
}
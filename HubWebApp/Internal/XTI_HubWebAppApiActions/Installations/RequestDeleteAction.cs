namespace XTI_HubWebAppApiActions.Installations;

public sealed class RequestDeleteAction : AppAction<InstallationIDRequest, EmptyActionResult>
{
    private readonly EfHubDB db;

    public RequestDeleteAction(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<EmptyActionResult> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken)
    {
        var efInstallation = await db.Installations.InstallationOrDefault(requestData.InstallationID, stoppingToken);
        await efInstallation.RequestDelete(stoppingToken);
        return new EmptyActionResult();
    }
}

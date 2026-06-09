namespace XTI_HubWebAppApiActions.App;

public sealed class DeleteInstallConfigurationAction : AppAction<InstallConfigurationIDRequest, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;

    public DeleteInstallConfigurationAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<EmptyActionResult> Execute(InstallConfigurationIDRequest requestData, CancellationToken stoppingToken)
    {
        var efApp = await appFromPath.Value(stoppingToken);
        var efConfiguration = await efApp.InstallConfiguration(requestData.ConfigurationID, stoppingToken);
        await efConfiguration.Delete(stoppingToken);
        return new EmptyActionResult();
    }
}

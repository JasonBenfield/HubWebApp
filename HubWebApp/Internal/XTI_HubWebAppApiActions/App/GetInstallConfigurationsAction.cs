namespace XTI_HubWebAppApiActions.App;

public sealed class GetInstallConfigurationsAction : AppAction<EmptyRequest, InstallConfigurationModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetInstallConfigurationsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<InstallConfigurationModel[]> Execute(EmptyRequest requestData, CancellationToken stoppingToken)
    {
        var efApp = await appFromPath.Value(stoppingToken);
        var efInstallConfigurations = await efApp.InstallConfigurations(stoppingToken);
        var installConfigurations = new List<InstallConfigurationModel>();
        foreach (var efInstallConfiguration in efInstallConfigurations)
        {
            var installConfiguration = await efInstallConfiguration.ToModel(stoppingToken);
            installConfigurations.Add(installConfiguration);
        }
        return installConfigurations.ToArray();
    }
}

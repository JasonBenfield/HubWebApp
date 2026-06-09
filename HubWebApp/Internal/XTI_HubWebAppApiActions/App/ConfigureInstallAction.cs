namespace XTI_HubWebAppApiActions.App;

public sealed class ConfigureInstallAction : AppAction<ConfigureAppInstallRequest, InstallConfigurationModel>
{
    private readonly AppFromPath appFromPath;
    private readonly EfHubDB db;

    public ConfigureInstallAction(AppFromPath appFromPath, EfHubDB db)
    {
        this.appFromPath = appFromPath;
        this.db = db;
    }

    public async Task<InstallConfigurationModel> Execute(ConfigureAppInstallRequest configRequest, CancellationToken stoppingToken)
    {
        var efApp = await appFromPath.Value(stoppingToken);
        var efTemplate = await db.InstallConfigurationTemplates.Template(configRequest.TemplateID, stoppingToken);
        var app = efApp.ToModel();
        var efInstallConfig = await db.InstallConfigurations.AddOrUpdateConfiguration
        (
            repoOwner: app.RepoOwner,
            repoName:  app.RepoName,
            configurationName: configRequest.ConfigurationName,
            appKey: app.AppKey,
            efTemplate: efTemplate,
            installSequence: configRequest.InstallSequence,
            ct: stoppingToken
        );
        var installConfig = await efInstallConfig.ToModel(stoppingToken);
        return installConfig;
    }
}

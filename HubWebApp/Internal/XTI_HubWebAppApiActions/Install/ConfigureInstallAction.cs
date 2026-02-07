namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class ConfigureInstallAction : AppAction<ConfigureInstallRequest, InstallConfigurationModel>
{
    private readonly IHubService hubAdmin;

    public ConfigureInstallAction(IHubService hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<InstallConfigurationModel> Execute(ConfigureInstallRequest configRequest, CancellationToken stoppingToken) =>
        hubAdmin.ConfigureInstall(configRequest, stoppingToken);
}

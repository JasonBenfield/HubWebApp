namespace XTI_HubWebAppApi.AppInstall;

internal sealed class ConfigureInstallAction : AppAction<ConfigureInstallRequest, InstallConfigurationModel>
{
    private readonly IHubAdministration hubAdmin;

    public ConfigureInstallAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<InstallConfigurationModel> Execute(ConfigureInstallRequest configRequest, CancellationToken stoppingToken) =>
        hubAdmin.ConfigureInstall(configRequest, stoppingToken);
}

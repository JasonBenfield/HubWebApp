namespace XTI_HubWebAppApi.AppInstall;

internal sealed class ConfigureInstallTemplateAction : AppAction<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel>
{
    private readonly IHubAdministration hubAdmin;

    public ConfigureInstallTemplateAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<InstallConfigurationTemplateModel> Execute(ConfigureInstallTemplateRequest configRequest, CancellationToken stoppingToken) =>
        hubAdmin.ConfigureInstallTemplate(configRequest, stoppingToken);
}

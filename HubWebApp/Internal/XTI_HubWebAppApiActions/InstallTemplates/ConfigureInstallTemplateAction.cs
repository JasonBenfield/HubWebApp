namespace XTI_HubWebAppApiActions.InstallTemplates;

public sealed class ConfigureInstallTemplateAction : AppAction<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel>
{
    private readonly IHubService hubAdmin;

    public ConfigureInstallTemplateAction(IHubService hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<InstallConfigurationTemplateModel> Execute(ConfigureInstallTemplateRequest configRequest, CancellationToken stoppingToken) =>
        hubAdmin.ConfigureInstallTemplate(configRequest, stoppingToken);
}

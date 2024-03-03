namespace XTI_HubWebAppApi.AppInstall;

internal sealed class GetInstallConfigurationsAction : AppAction<GetInstallConfigurationsRequest, InstallConfigurationModel[]>
{
    private readonly IHubAdministration hubAdmin;

    public GetInstallConfigurationsAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<InstallConfigurationModel[]> Execute(GetInstallConfigurationsRequest getRequest, CancellationToken stoppingToken) =>
        hubAdmin.InstallConfigurations(getRequest, stoppingToken);
}

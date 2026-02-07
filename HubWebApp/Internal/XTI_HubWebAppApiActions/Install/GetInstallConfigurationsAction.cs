namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class GetInstallConfigurationsAction : AppAction<GetInstallConfigurationsRequest, InstallConfigurationModel[]>
{
    private readonly IHubService hubAdmin;

    public GetInstallConfigurationsAction(IHubService hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<InstallConfigurationModel[]> Execute(GetInstallConfigurationsRequest getRequest, CancellationToken stoppingToken) =>
        hubAdmin.InstallConfigurations(getRequest, stoppingToken);
}

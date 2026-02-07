namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class GetInstallConfigurationAction : AppAction<InstallConfigurationIDRequest, InstallConfigurationModel>
{
    private readonly IHubService hubAdmin;

    public GetInstallConfigurationAction(IHubService hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<InstallConfigurationModel> Execute(InstallConfigurationIDRequest getRequest, CancellationToken stoppingToken) =>
        hubAdmin.InstallConfiguration(getRequest.ConfigurationID, stoppingToken);
}

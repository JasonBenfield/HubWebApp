namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class GetInstallConfigurationAction : AppAction<InstallConfigurationIDRequest, InstallConfigurationModel>
{
    private readonly IHubAdministration hubAdmin;

    public GetInstallConfigurationAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<InstallConfigurationModel> Execute(InstallConfigurationIDRequest getRequest, CancellationToken stoppingToken) =>
        hubAdmin.InstallConfiguration(getRequest.ConfigurationID, stoppingToken);
}

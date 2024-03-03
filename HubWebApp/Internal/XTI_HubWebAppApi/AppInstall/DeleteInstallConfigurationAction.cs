namespace XTI_HubWebAppApi.AppInstall;

internal sealed class DeleteInstallConfigurationAction : AppAction<DeleteInstallConfigurationRequest, EmptyActionResult>
{
    private readonly IHubAdministration hubAdmin;

    public DeleteInstallConfigurationAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public async Task<EmptyActionResult> Execute(DeleteInstallConfigurationRequest deleteRequest, CancellationToken stoppingToken)
    {
        await hubAdmin.DeleteInstallConfiguration(deleteRequest, stoppingToken);
        return new EmptyActionResult();
    }
}

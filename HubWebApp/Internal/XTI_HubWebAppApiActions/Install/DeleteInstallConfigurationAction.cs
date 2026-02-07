namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class DeleteInstallConfigurationAction : AppAction<DeleteInstallConfigurationRequest, EmptyActionResult>
{
    private readonly IHubService hubAdmin;

    public DeleteInstallConfigurationAction(IHubService hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public async Task<EmptyActionResult> Execute(DeleteInstallConfigurationRequest deleteRequest, CancellationToken stoppingToken)
    {
        await hubAdmin.DeleteInstallConfiguration(deleteRequest, stoppingToken);
        return new EmptyActionResult();
    }
}

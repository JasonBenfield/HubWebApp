namespace XTI_HubWebAppApiActions.Installations;

public sealed class InstalledAction : AppAction<InstallationIDRequest, EmptyActionResult>
{
    private readonly IHubService hubAdministration;

    public InstalledAction(IHubService hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<EmptyActionResult> Execute(InstallationIDRequest model, CancellationToken stoppingToken)
    {
        await hubAdministration.Installed(model.InstallationID, stoppingToken);
        return new EmptyActionResult();
    }
}
namespace XTI_HubWebAppApi.AppInstall;

public sealed class InstalledAction : AppAction<InstallationRequest, EmptyActionResult>
{
    private readonly IHubAdministration hubAdministration;

    public InstalledAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<EmptyActionResult> Execute(InstallationRequest model, CancellationToken stoppingToken)
    {
        await hubAdministration.Installed(model.InstallationID);
        return new EmptyActionResult();
    }
}
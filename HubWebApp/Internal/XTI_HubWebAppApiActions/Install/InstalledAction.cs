namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class InstalledAction : AppAction<GetInstallationRequest, EmptyActionResult>
{
    private readonly IHubAdministration hubAdministration;

    public InstalledAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<EmptyActionResult> Execute(GetInstallationRequest model, CancellationToken stoppingToken)
    {
        await hubAdministration.Installed(model.InstallationID, stoppingToken);
        return new EmptyActionResult();
    }
}
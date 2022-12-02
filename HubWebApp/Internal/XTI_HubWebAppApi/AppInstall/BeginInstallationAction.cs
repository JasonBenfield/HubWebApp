namespace XTI_HubWebAppApi.AppInstall;

public sealed class BeginInstallationAction : AppAction<GetInstallationRequest, EmptyActionResult>
{
    private readonly IHubAdministration hubAdministration;

    public BeginInstallationAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<EmptyActionResult> Execute(GetInstallationRequest model, CancellationToken stoppingToken)
    {
        await hubAdministration.BeginInstall(model.InstallationID);
        return new EmptyActionResult();
    }
}
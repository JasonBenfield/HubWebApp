namespace XTI_HubAppApi.AppInstall;

public sealed class BeginInstallationAction : AppAction<InstallationRequest, EmptyActionResult>
{
    private readonly IHubAdministration hubAdministration;

    public BeginInstallationAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<EmptyActionResult> Execute(InstallationRequest model)
    {
        await hubAdministration.BeginInstall(model.InstallationID);
        return new EmptyActionResult();
    }
}
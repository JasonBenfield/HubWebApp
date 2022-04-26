using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class InstalledAction : AppAction<InstalledRequest, EmptyActionResult>
{
    private readonly IHubAdministration hubAdministration;

    public InstalledAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<EmptyActionResult> Execute(InstalledRequest model)
    {
        await hubAdministration.Installed(model.InstallationID);
        return new EmptyActionResult();
    }
}
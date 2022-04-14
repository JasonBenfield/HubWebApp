using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class NewInstallationAction : AppAction<NewInstallationRequest, NewInstallationResult>
{
    private readonly IHubAdministration hubAdministration;

    public NewInstallationAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<NewInstallationResult> Execute(NewInstallationRequest model)
    {
        var result = await hubAdministration.NewInstallation
        (
            model.AppKey,
            model.QualifiedMachineName
        );
        return result;
    }
}
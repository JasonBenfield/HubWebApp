using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class BeginCurrentInstallationAction : AppAction<BeginInstallationRequest, int>
{
    private readonly IHubAdministration hubAdministration;

    public BeginCurrentInstallationAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public Task<int> Execute(BeginInstallationRequest model)
    {
        var versionToInstallKey = AppVersionKey.Parse(model.VersionKey);
        return hubAdministration.BeginCurrentInstall(model.AppKey, versionToInstallKey, model.QualifiedMachineName);
    }
}
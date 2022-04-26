using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class BeginVersionInstallationAction : AppAction<BeginInstallationRequest, int>
{
    private readonly IHubAdministration hubAdministration;

    public BeginVersionInstallationAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public Task<int> Execute(BeginInstallationRequest model) =>
        hubAdministration.BeginVersionInstall(model.AppKey, model.VersionKey, model.QualifiedMachineName);
}
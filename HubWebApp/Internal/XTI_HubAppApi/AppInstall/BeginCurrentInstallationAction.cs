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

    public Task<int> Execute(BeginInstallationRequest model) =>
        hubAdministration.BeginCurrentInstall(model.AppKey, model.VersionKey, model.QualifiedMachineName, model.Domain);
}
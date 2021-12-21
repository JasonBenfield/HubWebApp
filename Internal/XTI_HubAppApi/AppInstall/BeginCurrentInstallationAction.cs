using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall;

public sealed class BeginCurrentInstallationAction : AppAction<BeginInstallationRequest, int>
{
    private readonly AppFactory appFactory;

    public BeginCurrentInstallationAction(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public Task<int> Execute(BeginInstallationRequest model)
    {
        var versionToInstallKey = AppVersionKey.Parse(model.VersionKey);
        var installationService = new DbInstallationService
        (
            appFactory,
            model.AppKey,
            versionToInstallKey,
            model.QualifiedMachineName
        );
        return installationService.BeginCurrentInstall(versionToInstallKey);
    }
}
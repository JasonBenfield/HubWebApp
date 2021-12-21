using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall;

public sealed class BeginVersionInstallationAction : AppAction<BeginInstallationRequest, int>
{
    private readonly AppFactory appFactory;

    public BeginVersionInstallationAction(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public Task<int> Execute(BeginInstallationRequest model)
    {
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var installationService = new DbInstallationService
        (
            appFactory,
            model.AppKey,
            versionKey,
            model.QualifiedMachineName
        );
        return installationService.BeginVersionInstall();
    }
}
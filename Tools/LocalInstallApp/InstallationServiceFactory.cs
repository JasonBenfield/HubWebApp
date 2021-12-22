using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace LocalInstallApp;

internal class InstallationServiceFactory
{
    private readonly AppFactory appFactory;
    private readonly XTI_HubAppClient.HubAppClient hubClient;
    private readonly AppKey appKey;
    private readonly AppVersionKey versionKey;
    private readonly string machineName;

    public InstallationServiceFactory(AppFactory appFactory, XTI_HubAppClient.HubAppClient hubClient, IOptions<InstallOptions> options)
    {
        this.appFactory = appFactory;
        this.hubClient = hubClient;
        appKey = new AppKey(new AppName(options.Value.AppName), AppType.Values.Value(options.Value.AppType));
        versionKey = AppVersionKey.Parse(options.Value.VersionKey);
        machineName = options.Value.MachineName;
    }

    public InstallationService CreateHubApiInstallationService() =>
        new DbInstallationService(appFactory, appKey, versionKey, machineName);

    public InstallationService CreateHubClientInstallationService() =>
        new HubClientInstallationService(hubClient, appKey, versionKey, machineName);
}
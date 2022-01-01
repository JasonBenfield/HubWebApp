using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;

namespace LocalInstallApp;

internal sealed class HubClientInstallationService : InstallationService
{
    private readonly HubAppClient hubClient;

    public HubClientInstallationService(HubAppClient hubClient, AppKey appKey, AppVersionKey installVersionKey, string machineName)
        : base(appKey, installVersionKey, machineName)
    {
        this.hubClient = hubClient;
    }

    protected override Task<int> _BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName)
    {
        var request = new BeginInstallationRequest
        {
            AppKey = appKey,
            VersionKey = installVersionKey.Value,
            QualifiedMachineName = machineName
        };
        return hubClient.Install.BeginCurrentInstallation("", request);
    }

    protected override Task<int> _BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName)
    {
        var request = new BeginInstallationRequest
        {
            AppKey = appKey,
            VersionKey = versionKey.Value,
            QualifiedMachineName = machineName
        };
        return hubClient.Install.BeginVersionInstallation("", request);
    }

    protected override Task _Installed(int installationID)
        => hubClient.Install.Installed("", new InstalledRequest { InstallationID = installationID });
}
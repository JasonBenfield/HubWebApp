using System.Net.NetworkInformation;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;
using XTI_Installation;

namespace XTI_SupportServiceAppApi.Installations;

public sealed class ExecuteInstallationActivitiesAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly HubAppClient hubClient;
    private readonly XtiFolder xtiFolder;
    private readonly XtiEnvironment xtiEnv;

    public ExecuteInstallationActivitiesAction(HubAppClient hubClient, XtiFolder xtiFolder, XtiEnvironment xtiEnv)
    {
        this.hubClient = hubClient;
        this.xtiFolder = xtiFolder;
        this.xtiEnv = xtiEnv;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken ct)
    {
        var domain = IPGlobalProperties.GetIPGlobalProperties().DomainName;
        var machineNames = new List<string> { Environment.MachineName };
        if (!string.IsNullOrWhiteSpace(domain))
        {
            machineNames.Add($"{Environment.MachineName}.{domain}");
        }
        var installationActivities = await hubClient.Installations.GetInstallationActivities
        (
            new GetInstallationActivitiesRequest(machineNames.ToArray()),
            ct
        );
        foreach(var pendingInstallation in installationActivities.Installations)
        {

        }
        foreach (var pendingDelete in installationActivities.Deletions)
        {
            await hubClient.Installations.BeginDelete(new GetInstallationRequest(pendingDelete.Installation.ID), ct);
            var versionKey = pendingDelete.Installation.IsCurrent
                ? AppVersionKey.Current
                : pendingDelete.Version.VersionKey;
            if (pendingDelete.IsWebApp())
            {
                var iisWebSite = new IisWebSite
                (
                    xtiFolder,
                    xtiEnv,
                    pendingDelete.App.AppKey,
                    versionKey,
                    pendingDelete.Installation.SiteName
                );
                await iisWebSite.Delete();
            }
            else if (pendingDelete.IsServiceApp() && pendingDelete.Installation.IsCurrent)
            {
                var winService = new WinServiceInstallation(xtiFolder, xtiEnv, pendingDelete.App.AppKey);
                await winService.Delete();
            }
            var appFolder = xtiFolder.InstallPath(pendingDelete.App.AppKey, versionKey);
            if (Directory.Exists(appFolder))
            {
                Directory.Delete(appFolder, true);
            }
            await hubClient.Installations.Deleted(new GetInstallationRequest(pendingDelete.Installation.ID), ct);
        }
        return new EmptyActionResult();
    }
}
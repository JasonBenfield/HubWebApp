using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_Installation;

public abstract class InstallAppProcess
{
    protected readonly XtiFolder xtiFolder;
    protected readonly IHubAdministration hubAdministration;

    protected InstallAppProcess(XtiFolder xtiFolder, IHubAdministration hubAdministration)
    {
        this.xtiFolder = xtiFolder;
        this.hubAdministration = hubAdministration;
    }

    public async Task Run(string publishedAppPath, AppVersionInstallationModel versionInstallation, CancellationToken ct)
    {
        await hubAdministration.BeginInstall(versionInstallation.Installation.ID, ct);
        await _Run(publishedAppPath, versionInstallation, ct);
        await hubAdministration.Installed(versionInstallation.Installation.ID, ct);
        await WriteInstallationID(versionInstallation.Installation.ID, versionInstallation.App.AppKey, versionInstallation.GetVersionKey());
    }

    protected abstract Task _Run(string publishedAppPath, AppVersionInstallationModel versionInstallation, CancellationToken ct);

    private async Task WriteInstallationID(int installationID, AppKey appKey, AppVersionKey versionKey)
    {
        var serializedInstallationID = XtiSerializer.Serialize
        (
            new { InstallationID = installationID }
        );
        var installDir = xtiFolder.InstallPath(appKey, versionKey);
        using var writer = new StreamWriter(Path.Combine(installDir, "installation.json"), false);
        await writer.WriteAsync(serializedInstallationID);
    }
}


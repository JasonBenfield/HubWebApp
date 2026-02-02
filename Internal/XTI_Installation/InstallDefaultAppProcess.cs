using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_Installation;

public sealed class InstallDefaultAppProcess : InstallAppProcess
{
    internal InstallDefaultAppProcess(XtiFolder xtiFolder, IHubAdministration hubAdministration) : 
        base(xtiFolder, hubAdministration)
    {
    }

    protected override Task _Run(string publishedAppDir, AppVersionInstallationModel versionInstallation, CancellationToken ct) =>
        new CopyToInstallDirProcess(xtiFolder).Run
        (
            publishedAppDir,
            versionInstallation.App.AppKey,
            versionInstallation.GetVersionKey(),
            true
        );

}
using XTI_App.Extensions;
using XTI_Core;

namespace XTI_Installation;

public sealed class InstallDefaultAppProcess : InstallAppProcess
{
    private readonly XtiFolder xtiFolder;

    public InstallDefaultAppProcess(XtiFolder xtiFolder)
    {
        this.xtiFolder = xtiFolder;
    }

    public Task Run(string publishedAppDir, RequestedInstallation requestedInstallation, CancellationToken ct)
    {
        var installDir = xtiFolder.InstallPath(requestedInstallation.AppKey, requestedInstallation.VersionKey);
        return requestedInstallation.RunStep
        (
            $"Copy '{publishedAppDir}' to '{installDir}'",
            () => new CopyToInstallDirProcess(xtiFolder).Run
            (
                publishedAppDir,
                requestedInstallation.AppKey,
                requestedInstallation.VersionKey,
                true
            ),
            ct
        );
    }
}
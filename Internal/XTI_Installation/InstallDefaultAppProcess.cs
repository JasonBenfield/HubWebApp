using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Installation;

public sealed class InstallDefaultAppProcess : InstallAppProcess
{
    private readonly XtiFolder xtiFolder;

    public InstallDefaultAppProcess(XtiFolder xtiFolder)
    {
        this.xtiFolder = xtiFolder;
    }

    public Task Run(string publishedAppDir, InstallConfigurationModel installConfig, AppVersionKey installVersionKey, CancellationToken ct) =>
        new CopyToInstallDirProcess(xtiFolder).Run
        (
            publishedAppDir,
            installConfig.AppKey,
            installVersionKey,
            true
        );

}
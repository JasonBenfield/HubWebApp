using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Secrets;

namespace XTI_Installation;

public sealed class InstallServiceAppProcess : InstallAppProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiFolder xtiFolder;
    private readonly ISecretCredentialsFactory credentialsFactory;

    public InstallServiceAppProcess(XtiEnvironment xtiEnv, XtiFolder xtiFolder, ISecretCredentialsFactory credentialsFactory)
    {
        this.xtiEnv = xtiEnv;
        this.xtiFolder = xtiFolder;
        this.credentialsFactory = credentialsFactory;
    }

    public async Task Run(string publishedAppDir, RequestedInstallation requestedInstallation, CancellationToken ct)
    {
        WinServiceInstallation? winService = null;
        var startService = false;
        if (requestedInstallation.IsCurrent)
        {
            winService = new WinServiceInstallation(xtiFolder, xtiEnv, requestedInstallation.AppKey);
            if (!winService.Exists())
            {
                await requestedInstallation.RunStep
                (
                    "Create Service",
                    async () =>
                    {
                        var credentials = await credentialsFactory.Create("ServiceApp").Value();
                        await winService.Create(credentials.UserName, credentials.Password);
                    },
                    ct
                );
                startService = true;
            }
            else if (winService.IsRunning())
            {
                await requestedInstallation.RunStep
                (
                    "Stop Service",
                    () =>
                    {
                        winService.StopService();
                        return Task.CompletedTask;
                    },
                    ct
                );
                startService = true;
            }
        }
        var installDir = xtiFolder.InstallPath(requestedInstallation.AppKey, requestedInstallation.VersionKey);
        await requestedInstallation.RunStep
        (
            $"Copy '{publishedAppDir}' to '{installDir}'",
            () => new CopyToInstallDirProcess(xtiFolder).Run
            (
                publishedAppDir,
                requestedInstallation.AppKey,
                AppVersionKey.Current,
                true
            ),
            ct
        );
        if (winService != null && startService)
        {
            await requestedInstallation.RunStep
            (
                "Start Service",
                () =>
                {
                    winService.StartService();
                    return Task.CompletedTask;
                },
                ct
            );
        }
    }

}
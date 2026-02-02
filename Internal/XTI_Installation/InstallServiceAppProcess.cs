using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_Secrets;

namespace XTI_Installation;

public sealed class InstallServiceAppProcess : InstallAppProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly ISecretCredentialsFactory credentialsFactory;

    internal InstallServiceAppProcess(XtiFolder xtiFolder, IHubAdministration hubAdministration, XtiEnvironment xtiEnv, ISecretCredentialsFactory credentialsFactory)
        : base(xtiFolder, hubAdministration)
    {
        this.xtiEnv = xtiEnv;
        this.credentialsFactory = credentialsFactory;
    }

    protected override async Task _Run(string publishedAppDir, AppVersionInstallationModel versionInstallation, CancellationToken ct)
    {
        WinServiceInstallation? winService = null;
        var startService = false;
        if (versionInstallation.IsCurrent())
        {
            winService = new WinServiceInstallation(xtiFolder, xtiEnv, versionInstallation.App.AppKey);
            if (!winService.Exists())
            {
                var credentials = await credentialsFactory.Create("ServiceApp").Value();
                await winService.Create(credentials.UserName, credentials.Password);
                startService = true;
            }
            else if (winService.IsRunning())
            {
                winService.StopService();
                startService = true;
            }
        }
        await new CopyToInstallDirProcess(xtiFolder).Run
        (
            publishedAppDir,
            versionInstallation.App.AppKey,
            versionInstallation.GetVersionKey(),
            true
        );
        if (winService != null && startService)
        {
            winService.StartService();
        }
    }

}
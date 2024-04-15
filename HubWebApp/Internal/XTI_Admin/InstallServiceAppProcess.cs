using System.Diagnostics.CodeAnalysis;
using System.ServiceProcess;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Credentials;
using XTI_Processes;
using XTI_Secrets;
using XTI_ServiceAppInstallation;

namespace XTI_Admin;

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

    public async Task Run(string publishedAppDir, AdminInstallOptions adminInstOptions, AppVersionKey installVersionKey)
    {
        WinServiceInstallation? winService = null;
        var startService = false;
        if (installVersionKey.IsCurrent())
        {
            winService = new WinServiceInstallation(xtiFolder, xtiEnv, adminInstOptions.AppKey);
            if (!winService.Exists())
            {
                Console.WriteLine($"Creating service '{adminInstOptions.AppKey.Name.DisplayText}'");
                var secretCredentialsValue = await RetrieveCredentials("ServiceApp");
                await winService.Create(secretCredentialsValue.UserName, secretCredentialsValue.Password);
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
            adminInstOptions.AppKey,
            installVersionKey,
            true
        );
        if (winService != null && startService)
        {
            Console.WriteLine($"Starting service '{adminInstOptions.AppKey.Name.DisplayText}'");
            winService.StartService();
        }
    }

    private Task<CredentialValue> RetrieveCredentials(string credentialKey) =>
        credentialsFactory.Create(credentialKey).Value();

}
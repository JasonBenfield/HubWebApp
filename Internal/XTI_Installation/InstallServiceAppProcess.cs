using XTI_App.Abstractions;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub.Abstractions;
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

    public async Task Run(string publishedAppDir, InstallConfigurationModel installConfig, AppVersionKey installVersionKey, CancellationToken ct)
    {
        WinServiceInstallation? winService = null;
        var startService = false;
        if (installVersionKey.IsCurrent())
        {
            winService = new WinServiceInstallation(xtiFolder, xtiEnv, installConfig.AppKey);
            if (!winService.Exists())
            {
                Console.WriteLine($"Creating service '{installConfig.AppKey.Name.DisplayText}'");
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
            installConfig.AppKey,
            installVersionKey,
            true
        );
        if (winService != null && startService)
        {
            Console.WriteLine($"Starting service '{installConfig.AppKey.Name.DisplayText}'");
            winService.StartService();
        }
    }

    private Task<CredentialValue> RetrieveCredentials(string credentialKey) =>
        credentialsFactory.Create(credentialKey).Value();

}
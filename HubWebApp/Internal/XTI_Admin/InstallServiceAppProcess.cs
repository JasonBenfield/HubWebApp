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

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Windows Service")]
internal sealed class InstallServiceAppProcess :  InstallAppProcess
{
    private readonly Scopes scopes;

    public InstallServiceAppProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run(string publishedAppDir, AdminInstallOptions adminInstOptions, AppVersionKey installVersionKey)
    {
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        WinServiceInstallation? winService = null;
        var startService = false;
        if (installVersionKey.IsCurrent())
        {
            var xtiFolder = scopes.GetRequiredService<XtiFolder>();
            winService = new WinServiceInstallation(xtiFolder, xtiEnv, adminInstOptions.AppKey);
            if (!winService.Exists())
            {
                Console.WriteLine($"Creating service '{adminInstOptions.AppKey.Name.DisplayText}'");
                var secretCredentialsValue = await retrieveCredentials("ServiceApp");
                await winService.Create(secretCredentialsValue.UserName, secretCredentialsValue.Password);
                startService = true;
            }
            else if (winService.IsRunning())
            {
                winService.StopService();
                startService = true;
            }
        }
        await new CopyToInstallDirProcess(scopes).Run(publishedAppDir, adminInstOptions.AppKey, installVersionKey, true);
        if (winService != null && startService)
        {
            Console.WriteLine($"Starting service '{adminInstOptions.AppKey.Name.DisplayText}'");
            winService.StartService();
        }
    }

    private async Task<CredentialValue> retrieveCredentials(string credentialKey)
    {
        var credentialsFactory = scopes.GetRequiredService<ISecretCredentialsFactory>();
        var credentials = await credentialsFactory.Create(credentialKey).Value();
        return credentials;
    }

    private static ServiceController? getService(string serviceName) =>
        ServiceController
            .GetServices(".")
            .FirstOrDefault
            (
                c => c.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase)
            );

}
using System.Diagnostics.CodeAnalysis;
using System.ServiceProcess;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Credentials;
using XTI_Processes;
using XTI_Secrets;

namespace XTI_Admin;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Windows Service")]
internal sealed class InstallServiceProcess
{
    private readonly Scopes scopes;

    public InstallServiceProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run(string publishedAppDir, AppKey appKey, AppVersionKey installVersionKey)
    {
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        ServiceController? sc = null;
        if (installVersionKey.Equals(AppVersionKey.Current))
        {
            var xtiFolder = scopes.GetRequiredService<XtiFolder>();
            var appName = appKey.Name.DisplayText.Replace(" ", "");
            var serviceName = $"Xti_{xtiEnv.EnvironmentName}_{appName}";
            sc = getService(serviceName);
            if (sc == null)
            {
                var binPath = Path.Combine
                (
                    xtiFolder.InstallPath(appKey, AppVersionKey.Current),
                    $"{appName}ServiceApp.exe"
                );
                binPath = $"{binPath} --Environment {xtiEnv.EnvironmentName}";
                Console.WriteLine($"Creating service '{binPath}'");
                var secretCredentialsValue = await retrieveCredentials("ServiceApp");
                var createServiceProcess = new WinProcess("sc")
                    .WriteOutputToConsole()
                    .UseArgumentNameDelimiter("")
                    .AddArgument("create")
                    .AddArgument(serviceName)
                    .UseArgumentValueDelimiter("= ")
                    .AddArgument("start", "auto")
                    .AddArgument("binpath", new Quoted(binPath))
                    .AddArgument("obj", new Quoted(secretCredentialsValue.UserName))
                    .AddArgument("password", new Quoted(secretCredentialsValue.Password));
                Console.WriteLine(createServiceProcess.CommandText());
                var createServiceResult = await createServiceProcess
                    .Run();
                createServiceResult.EnsureExitCodeIsZero();
                sc = getService(serviceName);
            }
            else if (sc.Status == ServiceControllerStatus.Running)
            {
                Console.WriteLine($"Stopping services '{sc.DisplayName}'");
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }
        await new CopyToInstallDirProcess(scopes).Run(publishedAppDir, appKey, installVersionKey, true);
        if (sc != null)
        {
            Console.WriteLine($"Starting services '{sc.DisplayName}'");
            sc.Start();
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
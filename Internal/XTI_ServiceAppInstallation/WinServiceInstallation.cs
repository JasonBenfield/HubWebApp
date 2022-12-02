using System.Diagnostics.CodeAnalysis;
using System.ServiceProcess;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Processes;

namespace XTI_ServiceAppInstallation;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Windows Service")]
public sealed class WinServiceInstallation
{
    private readonly XtiFolder xtiFolder;
    private readonly XtiEnvironment xtiEnv;
    private readonly AppKey appKey;
    private ServiceController? sc;

    public WinServiceInstallation(XtiFolder xtiFolder, XtiEnvironment xtiEnv, AppKey appKey)
    {
        this.xtiFolder = xtiFolder;
        this.xtiEnv = xtiEnv;
        this.appKey = appKey;
    }

    public bool Exists() =>
        GetService() != null;

    public bool IsRunning() =>
        GetService()?.Status == ServiceControllerStatus.Running;

    public async Task Create(string userName, string password)
    {
        var appName = appKey.Name.DisplayText.Replace(" ", "");
        var serviceName = $"Xti_{xtiEnv.EnvironmentName}_{appName}";
        var binPath = Path.Combine
        (
            xtiFolder.InstallPath(appKey, AppVersionKey.Current),
            $"{appName}ServiceApp.exe"
        );
        binPath = $"{binPath} --Environment {xtiEnv.EnvironmentName}";
        var createServiceProcess = new WinProcess("sc")
            .WriteOutputToConsole()
            .UseArgumentNameDelimiter("")
            .AddArgument("create")
            .AddArgument(serviceName)
            .UseArgumentValueDelimiter("= ")
            .AddArgument("start", "auto")
            .AddArgument("binpath", new Quoted(binPath))
            .AddArgument("obj", new Quoted(userName))
            .AddArgument("password", new Quoted(password));
        var createServiceResult = await createServiceProcess.Run();
        createServiceResult.EnsureExitCodeIsZero();
    }

    public void StopService()
    {
        var sc = GetService();
        if (sc != null)
        {
            sc.Stop();
            sc.WaitForStatus(ServiceControllerStatus.Stopped);
        }
    }

    public void StartService()
    {
        var sc = GetService();
        if (sc != null)
        {
            sc.Start();
        }
    }

    public async Task Delete()
    {
        var sc = GetService();
        if (sc != null)
        {
            sc.Stop();
            sc.WaitForStatus(ServiceControllerStatus.Stopped);
            var deleteServiceProcess = new WinProcess("sc")
                .WriteOutputToConsole()
                .UseArgumentNameDelimiter("")
                .AddArgument("delete")
                .AddArgument(GetServiceName());
            var createServiceResult = await deleteServiceProcess.Run();
            createServiceResult.EnsureExitCodeIsZero();
            this.sc = null;
        }
    }

    private ServiceController? GetService()
    {
        if (sc == null)
        {
            var serviceName = GetServiceName();
            sc = ServiceController
                .GetServices(".")
                .FirstOrDefault
                (
                    c => c.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase)
                );
        }
        return sc;
    }

    private string GetServiceName()
    {
        var appName = GetAppName();
        return $"Xti_{xtiEnv.EnvironmentName}_{appName}";
    }

    private string GetAppName() => appKey.Name.DisplayText.Replace(" ", "");
}
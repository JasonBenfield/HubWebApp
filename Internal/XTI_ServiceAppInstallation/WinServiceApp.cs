using System.Diagnostics.CodeAnalysis;
using System.ServiceProcess;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Processes;

namespace XTI_ServiceAppInstallation;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Windows Service")]
public sealed class WinServiceApp
{
    private readonly XtiEnvironment xtiEnv;
    private readonly AppKey appKey;
    private ServiceController? sc;

    public WinServiceApp(XtiEnvironment xtiEnv, AppKey appKey)
    {
        this.xtiEnv = xtiEnv;
        this.appKey = appKey;
    }

    public bool Exists() =>
        GetService() != null;

    public bool IsRunning() =>
        GetService()?.Status == ServiceControllerStatus.Running;

    public async Task RestartService()
    {
        var sc = GetService();
        if (sc != null)
        {
            if(sc.Status == ServiceControllerStatus.Running)
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
            sc.Start();
        }
    }

    public void StopService()
    {
        var sc = GetService();
        if (sc != null)
        {
            if (sc.Status == ServiceControllerStatus.Running)
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }
    }

    public void StartService()
    {
        var sc = GetService();
        if (sc != null)
        {
            if (sc.Status != ServiceControllerStatus.Running)
            {
                sc.Start();
            }
        }
    }

    public async Task Delete()
    {
        var sc = GetService();
        if (sc != null)
        {
            sc.Stop();
            sc.WaitForStatus(ServiceControllerStatus.Stopped);
            var serviceName = new WinServiceName(xtiEnv, appKey).Value;
            var deleteServiceProcess = new WinProcess("sc")
                .WriteOutputToConsole()
                .UseArgumentNameDelimiter("")
                .AddArgument("delete")
                .AddArgument(serviceName);
            var createServiceResult = await deleteServiceProcess.Run();
            createServiceResult.EnsureExitCodeIsZero();
            this.sc = null;
        }
    }

    private ServiceController? GetService()
    {
        if (sc == null)
        {
            var serviceName = new WinServiceName(xtiEnv, appKey).Value;
            sc = ServiceController
                .GetServices(".")
                .FirstOrDefault
                (
                    c => c.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase)
                );
        }
        return sc;
    }
}

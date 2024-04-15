using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Processes;

namespace XTI_ServiceAppInstallation;

public sealed class WinServiceInstallation
{
    private readonly XtiFolder xtiFolder;
    private readonly XtiEnvironment xtiEnv;
    private readonly AppKey appKey;
    private readonly WinServiceApp app;

    public WinServiceInstallation(XtiFolder xtiFolder, XtiEnvironment xtiEnv, AppKey appKey)
    {
        this.xtiFolder = xtiFolder;
        this.xtiEnv = xtiEnv;
        this.appKey = appKey;
        app = new WinServiceApp(xtiEnv, appKey);
    }

    public bool Exists() => app.Exists();

    public bool IsRunning() => app.IsRunning();

    public async Task Create(string userName, string password)
    {
        var appName = appKey.Name.DisplayText.Replace(" ", "");
        var serviceName = new WinServiceName(xtiEnv, appKey).Value;
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

    public void StopService() => app.StopService();

    public void StartService() => app.StartService();

    public Task Delete() => app.Delete();
}
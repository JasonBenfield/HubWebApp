using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_Installation;

namespace XTI_Admin;

public sealed class LocalInstallProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly InstallAppProcessFactory installFactory;

    public LocalInstallProcess(XtiEnvironment xtiEnv, AppVersionNameAccessor versionNameAccessor, InstallAppProcessFactory installFactory)
    {
        this.xtiEnv = xtiEnv;
        this.versionNameAccessor = versionNameAccessor;
        this.installFactory = installFactory;
    }

    public async Task Run(NewInstallationResult newInstallation, IPublishedAssets publishedAssets, CancellationToken ct)
    {
        var appKey = newInstallation.App.AppKey;
        var versionKey = AppVersionKey.Current;
        if (xtiEnv.IsProduction())
        {
            versionKey = newInstallation.Version.VersionKey;
        }
        Console.WriteLine($"Starting install {appKey.Format()} {versionKey}");
        var release = newInstallation.GetRelease();
        var setupAppPath = await publishedAssets.LoadSetup(release, appKey, versionKey, ct);
        var publishedAppPath = await publishedAssets.LoadApps(release, appKey, versionKey, ct);
        var versionName = versionNameAccessor.Value;
        await new RunSetupProcess(xtiEnv).Run(versionName, appKey, versionKey, setupAppPath);
        var installAppProcess = installFactory.Create(appKey);
        if (xtiEnv.IsProduction())
        {
            var versionInstallation = newInstallation.GetVersionInstallation();
            Console.WriteLine($"Installing {versionInstallation.App.AppKey.Format()} {versionInstallation.GetVersionKey().DisplayText} to website {versionInstallation.Installation.SiteName}");
            await installAppProcess.Run(publishedAppPath, versionInstallation, ct);
            Console.WriteLine($"Installed {versionInstallation.App.AppKey.Format()} {versionInstallation.GetVersionKey().DisplayText}.");
        }
        var currentVersionInstallation = newInstallation.GetCurrentInstallation();
        Console.WriteLine($"Installing {currentVersionInstallation.App.AppKey.Format()} {currentVersionInstallation.GetVersionKey().DisplayText} to website {currentVersionInstallation.Installation.SiteName}...");
        await installAppProcess.Run(publishedAppPath, currentVersionInstallation, ct);
        Console.WriteLine($"Installed {currentVersionInstallation.App.AppKey.Format()} {currentVersionInstallation.GetVersionKey().DisplayText}.");
    }
}
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_Installation;

namespace XTI_Admin;

public sealed class LocalInstallProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly IHubAdministration hubAdministration;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly XtiFolder xtiFolder;
    private readonly InstallAppProcessFactory installFactory;

    public LocalInstallProcess(XtiEnvironment xtiEnv, IHubAdministration hubAdministration, AppVersionNameAccessor versionNameAccessor, XtiFolder xtiFolder, InstallAppProcessFactory installFactory)
    {
        this.xtiEnv = xtiEnv;
        this.hubAdministration = hubAdministration;
        this.versionNameAccessor = versionNameAccessor;
        this.xtiFolder = xtiFolder;
        this.installFactory = installFactory;
    }

    public async Task Run(InstallConfigurationModel installConfig, AdminInstallOptions adminInstOptions, IPublishedAssets publishedAssets, CancellationToken ct)
    {
        var appKey = installConfig.AppKey;
        var versionKey = AppVersionKey.Current;
        if (xtiEnv.IsProduction() && !adminInstOptions.VersionKey.Equals(AppVersionKey.None))
        {
            versionKey = adminInstOptions.VersionKey;
        }
        Console.WriteLine($"Starting install {appKey.Name.DisplayText} {appKey.Type.DisplayText} {versionKey}");
        var setupAppPath = await publishedAssets.LoadSetup(adminInstOptions.Release, appKey, versionKey, ct);
        var appPath = await publishedAssets.LoadApps(adminInstOptions.Release, appKey, versionKey, ct);
        var versionName = versionNameAccessor.Value;
        await new RunSetupProcess(xtiEnv).Run(versionName, appKey, adminInstOptions.VersionKey, setupAppPath);
        var installAppProcess = installFactory.Create(appKey);
        if (xtiEnv.IsProduction())
        {
            await hubAdministration.BeginInstall(adminInstOptions.VersionInstallationID, ct);
            await installAppProcess.Run(appPath, installConfig, versionKey, ct);
            await hubAdministration.Installed(adminInstOptions.VersionInstallationID, ct);
            await WriteInstallationID(adminInstOptions.VersionInstallationID, appKey, versionKey);
        }
        await hubAdministration.BeginInstall(adminInstOptions.CurrentInstallationID, ct);
        await installAppProcess.Run(appPath, installConfig, AppVersionKey.Current, ct);
        await hubAdministration.Installed(adminInstOptions.CurrentInstallationID, ct);
        await WriteInstallationID(adminInstOptions.CurrentInstallationID, appKey, AppVersionKey.Current);
        Console.WriteLine("Installation Complete");
    }

    private async Task WriteInstallationID(int installationID, AppKey appKey, AppVersionKey versionKey)
    {
        var serializedInstallationID = XtiSerializer.Serialize
        (
            new { InstallationID = installationID }
        );
        var installDir = xtiFolder.InstallPath(appKey, versionKey);
        using var writer = new StreamWriter(Path.Combine(installDir, "installation.json"), false);
        await writer.WriteAsync(serializedInstallationID);
    }
}
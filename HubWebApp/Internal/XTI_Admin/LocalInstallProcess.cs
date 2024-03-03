using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class LocalInstallProcess
{
    private readonly InstallationUserCredentials credentials;
    private readonly XtiEnvironment xtiEnv;
    private readonly IHubAdministration hubAdministration;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly XtiFolder xtiFolder;
    private readonly InstallWebAppProcess installWebAppProcess;
    private readonly InstallServiceAppProcess installServiceAppProcess;

    public LocalInstallProcess(InstallationUserCredentials credentials, XtiEnvironment xtiEnv, IHubAdministration hubAdministration, AppVersionNameAccessor versionNameAccessor, XtiFolder xtiFolder, InstallWebAppProcess installWebAppProcess, InstallServiceAppProcess installServiceAppProcess)
    {
        this.credentials = credentials;
        this.xtiEnv = xtiEnv;
        this.hubAdministration = hubAdministration;
        this.versionNameAccessor = versionNameAccessor;
        this.xtiFolder = xtiFolder;
        this.installWebAppProcess = installWebAppProcess;
        this.installServiceAppProcess = installServiceAppProcess;
    }

    public async Task Run(AdminInstallOptions adminInstOptions, IPublishedAssets publishedAssets, CancellationToken ct)
    {
        var installerCreds = new CredentialValue
        (
            adminInstOptions.InstallerUserName,
            adminInstOptions.InstallerPassword
        );
        await credentials.Update(installerCreds);
        var appKey = adminInstOptions.AppKey;
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
        var installAppProcess = GetInstallAppProcess(appKey);
        if (xtiEnv.IsProduction())
        {
            await hubAdministration.BeginInstall(adminInstOptions.VersionInstallationID, ct);
            await installAppProcess.Run(appPath, adminInstOptions, versionKey);
            await hubAdministration.Installed(adminInstOptions.VersionInstallationID, ct);
            await WriteInstallationID(adminInstOptions.VersionInstallationID, appKey, versionKey);
        }
        await hubAdministration.BeginInstall(adminInstOptions.CurrentInstallationID, ct);
        await installAppProcess.Run(appPath, adminInstOptions, AppVersionKey.Current);
        await hubAdministration.Installed(adminInstOptions.CurrentInstallationID, ct);
        await WriteInstallationID(adminInstOptions.CurrentInstallationID, appKey, AppVersionKey.Current);
        Console.WriteLine("Installation Complete");
    }

    private InstallAppProcess GetInstallAppProcess(AppKey appKey)
    {
        InstallAppProcess installAppProcess;
        if (appKey.IsAppType(AppType.Values.WebApp))
        {
            installAppProcess = installWebAppProcess;
        }
        else if (appKey.IsAppType(AppType.Values.ServiceApp))
        {
            installAppProcess = installServiceAppProcess;
        }
        else
        {
            installAppProcess = new InstallDefaultAppProcess(xtiFolder);
        }
        return installAppProcess;
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
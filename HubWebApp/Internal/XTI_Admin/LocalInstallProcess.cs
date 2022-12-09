using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class LocalInstallProcess
{
    private readonly Scopes scopes;
    private readonly IPublishedAssets publishedAssets;

    public LocalInstallProcess(Scopes scopes, IPublishedAssets publishedAssets)
    {
        this.scopes = scopes;
        this.publishedAssets = publishedAssets;
    }

    public async Task Run(AdminInstallOptions adminInstOptions)
    {
        var installerCreds = new CredentialValue
        (
            adminInstOptions.InstallerUserName,
            adminInstOptions.InstallerPassword
        );
        var credentials = scopes.GetRequiredService<InstallationUserCredentials>();
        await credentials.Update(installerCreds);
        var appKey = adminInstOptions.AppKey;
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var versionKey = AppVersionKey.Current;
        if (xtiEnv.IsProduction() && !adminInstOptions.VersionKey.Equals(AppVersionKey.None))
        {
            versionKey = adminInstOptions.VersionKey;
        }
        Console.WriteLine($"Starting install {appKey.Name.DisplayText} {appKey.Type.DisplayText} {versionKey}");
        var setupAppPath = await publishedAssets.LoadSetup(adminInstOptions.Release, appKey, versionKey);
        var appPath = await publishedAssets.LoadApps(adminInstOptions.Release, appKey, versionKey);
        var versionName = scopes.GetRequiredService<AppVersionNameAccessor>().Value;
        await new RunSetupProcess(xtiEnv).Run(versionName, appKey, adminInstOptions.VersionKey, setupAppPath);
        var installAppProcess = getInstallAppProcess(appKey);
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        if (xtiEnv.IsProduction())
        {
            await hubAdministration.BeginInstall(adminInstOptions.VersionInstallationID);
            await installAppProcess.Run(appPath, adminInstOptions, versionKey);
            await hubAdministration.Installed(adminInstOptions.VersionInstallationID);
            await WriteInstallationID(adminInstOptions.VersionInstallationID, appKey, versionKey);
        }
        await hubAdministration.BeginInstall(adminInstOptions.CurrentInstallationID);
        await installAppProcess.Run(appPath, adminInstOptions, AppVersionKey.Current);
        await hubAdministration.Installed(adminInstOptions.CurrentInstallationID);
        await WriteInstallationID(adminInstOptions.CurrentInstallationID, appKey, AppVersionKey.Current);
        Console.WriteLine("Installation Complete");
    }

    private InstallAppProcess getInstallAppProcess(AppKey appKey)
    {
        InstallAppProcess installAppProcess;
        if (appKey.IsAppType(AppType.Values.WebApp))
        {
            installAppProcess = new InstallWebAppProcess(scopes);
        }
        else if (appKey.IsAppType(AppType.Values.ServiceApp))
        {
            installAppProcess = new InstallServiceAppProcess(scopes);
        }
        else
        {
            installAppProcess = new InstallDefaultAppProcess(scopes);
        }
        return installAppProcess;
    }

    private async Task WriteInstallationID(int installationID, AppKey appKey, AppVersionKey versionKey)
    {
        var serializedInstallationID = XtiSerializer.Serialize
        (
            new { InstallationID = installationID }
        );
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
        var installDir = xtiFolder.InstallPath(appKey, versionKey);
        using var writer = new StreamWriter(Path.Combine(installDir, "installation.json"), false);
        await writer.WriteAsync(serializedInstallationID);
    }
}
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;

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
        var appKey = adminInstOptions.AppKey;
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var versionKey = AppVersionKey.Current;
        if (xtiEnv.IsProduction() && !string.IsNullOrWhiteSpace(adminInstOptions.VersionKey))
        {
            versionKey = adminInstOptions.VersionKey;
        }
        Console.WriteLine($"Starting install {appKey.Name.DisplayText} {appKey.Type.DisplayText} {versionKey}");
        var setupAppPath = await publishedAssets.LoadSetup(adminInstOptions.Release, appKey, versionKey);
        var appPath = await publishedAssets.LoadApps(adminInstOptions.Release, appKey, versionKey);
        var versionName = scopes.GetRequiredService<AppVersionNameAccessor>().Value;
        await new RunSetupProcess(xtiEnv).Run(versionName, appKey, adminInstOptions.VersionKey, setupAppPath);
        if (appKey.Type.Equals(AppType.Values.WebApp))
        {
            if (xtiEnv.IsProduction())
            {
                await new InstallWebAppProcess(scopes).Run(appPath, appKey, versionKey, versionKey, adminInstOptions.Options);
            }
            await new InstallWebAppProcess(scopes).Run(appPath, appKey, versionKey, AppVersionKey.Current, adminInstOptions.Options);
        }
        else if (appKey.Type.Equals(AppType.Values.ServiceApp))
        {
            if (xtiEnv.IsProduction())
            {
                await new InstallServiceProcess(scopes).Run(appPath, appKey, versionKey);
            }
            await new InstallServiceProcess(scopes).Run(appPath, appKey, AppVersionKey.Current);
        }
        else if (appKey.Type.Equals(AppType.Values.ConsoleApp))
        {
            if (xtiEnv.IsProduction())
            {
                await new CopyToInstallDirProcess(scopes).Run(appPath, appKey, versionKey, true);
            }
            await new CopyToInstallDirProcess(scopes).Run(appPath, appKey, AppVersionKey.Current, true);
        }
        if (xtiEnv.IsProduction())
        {
            await WriteInstallationID(adminInstOptions.VersionInstallationID, appKey, versionKey);
        }
        await WriteInstallationID(adminInstOptions.CurrentInstallationID, appKey, AppVersionKey.Current);
        Console.WriteLine("Installation Complete");
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
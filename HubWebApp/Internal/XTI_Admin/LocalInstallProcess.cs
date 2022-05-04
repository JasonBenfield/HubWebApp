using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Credentials;

namespace XTI_Admin;

internal sealed class LocalInstallProcess
{
    private readonly Scopes scopes;
    private readonly AppKey appKey;
    private readonly IPublishedAssets publishedAssets;

    public LocalInstallProcess(Scopes scopes, AppKey appKey, IPublishedAssets publishedAssets)
    {
        this.scopes = scopes;
        this.appKey = appKey;
        this.publishedAssets = publishedAssets;
    }

    public async Task Run(InstallationOptions installation)
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        Console.WriteLine($"Starting install {appKey.Name.DisplayText} {appKey.Type.DisplayText} {options.VersionKey} {options.Release}");
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        await storeInstallationUserCredentials();
        var versionKey = AppVersionKey.Current;
        if (xtiEnv.IsProduction() && !string.IsNullOrWhiteSpace(options.VersionKey))
        {
            versionKey = AppVersionKey.Parse(options.VersionKey);
        }
        var setupAppPath = await publishedAssets.LoadSetup(appKey, versionKey);
        var appPath = await publishedAssets.LoadApps(appKey, versionKey);
        var versionName = scopes.GetRequiredService<AppVersionNameAccessor>().Value;
        await new RunSetupProcess(xtiEnv).Run(versionName, appKey, options.VersionKey, setupAppPath);
        if (appKey.Type.Equals(AppType.Values.WebApp))
        {
            if (xtiEnv.IsProduction())
            {
                await new InstallWebAppProcess(scopes).Run(appPath, appKey, versionKey, versionKey, installation);
            }
            await new InstallWebAppProcess(scopes).Run(appPath, appKey, versionKey, AppVersionKey.Current, installation);
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
        Console.WriteLine("Installation Complete");
    }

    private async Task storeInstallationUserCredentials()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        if (!string.IsNullOrWhiteSpace(options.InstallationUserName))
        {
            var credentials = scopes.GetRequiredService<InstallationUserCredentials>();
            await credentials.Update
            (
                new CredentialValue
                (
                    options.InstallationUserName,
                    options.InstallationPassword
                )
            );
        }
    }

}
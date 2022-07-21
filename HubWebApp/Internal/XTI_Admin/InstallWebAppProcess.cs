using Microsoft.Web.Administration;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub.Abstractions;
using XTI_Secrets;

namespace XTI_Admin;

internal sealed class InstallWebAppProcess : InstallAppProcess
{
    private readonly Scopes scopes;

    private static readonly string appOfflineFileName = "app_offline.htm";

    public InstallWebAppProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run(string publishedAppDir, AdminInstallOptions adminInstOptions, AppVersionKey installVersionKey)
    {
        Console.WriteLine($"Installing {adminInstOptions.AppKey.Name.DisplayText} {adminInstOptions.VersionKey.DisplayText} to website {adminInstOptions.Options.SiteName}");
        await prepareIis(adminInstOptions.AppKey, installVersionKey, adminInstOptions.Options.SiteName);
        try
        {
            deleteExistingWebFiles(adminInstOptions.AppKey, installVersionKey);
        }
        catch
        {
            await Task.Delay(TimeSpan.FromSeconds(15));
            deleteExistingWebFiles(adminInstOptions.AppKey, installVersionKey);
        }
        await new CopyToInstallDirProcess(scopes).Run(publishedAppDir, adminInstOptions.AppKey, installVersionKey, false);
        var appOfflinePath = getAppOfflinePath(adminInstOptions.AppKey, installVersionKey);
        File.Delete(appOfflinePath);
    }

    private void deleteExistingWebFiles(AppKey appKey, AppVersionKey installVersionKey)
    {
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
        var installDir = xtiFolder.InstallPath(appKey, installVersionKey);
        Console.WriteLine($"Deleting files in '{installDir}'");
        foreach (var file in Directory.GetFiles(installDir).Where(f => !Path.GetFileName(f).Equals(appOfflineFileName, StringComparison.OrdinalIgnoreCase)))
        {
            File.Delete(file);
        }
        foreach (var directory in Directory.GetDirectories(installDir))
        {
            Directory.Delete(directory, true);
        }
    }

    private async Task prepareIis(AppKey appKey, AppVersionKey versionKey, string siteName)
    {
        var secretCredentialsValue = await retrieveCredentials("WebApp");
        using var server = new ServerManager();
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        if (string.IsNullOrWhiteSpace(siteName))
        {
            throw new ArgumentException("siteName is required");
        }
        var site = server.Sites[siteName];
        var appName = appKey.Name.DisplayText.Replace(" ", "");
        if (appKey.Type.Equals(AppType.Values.WebService))
        {
            appName += "Service";
        }
        var appPoolName = $"Xti_{xtiEnv.EnvironmentName}_{appName}_{versionKey.DisplayText}";
        var appPool = server.ApplicationPools.FirstOrDefault(ap => ap.Name.Equals(appPoolName, StringComparison.OrdinalIgnoreCase));
        if (appPool == null)
        {
            Console.WriteLine($"Adding application pool '{appPoolName}'");
            var newAppPool = server.ApplicationPools.Add(appPoolName);
            newAppPool.ProcessModel.UserName = secretCredentialsValue.UserName;
            newAppPool.ProcessModel.Password = secretCredentialsValue.Password;
            newAppPool.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
            newAppPool.ManagedRuntimeVersion = "v4.0";
        }
        var baseApp = site.Applications
            .FirstOrDefault(a => a.Path == "/")
            ?? throw new Exception("App not found with path '/'");
        var virtDirPath = $"/{appName}";
        var virtDir = baseApp.VirtualDirectories.FirstOrDefault(vd => vd.Path.Equals(virtDirPath, StringComparison.OrdinalIgnoreCase));
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
        var virtDirPhysPath = xtiFolder.InstallPath(appKey);
        if (virtDir == null)
        {
            Console.WriteLine($"Adding virtual directory '{virtDirPath}'");
            baseApp.VirtualDirectories.Add(virtDirPath, virtDirPhysPath);
        }
        var appPhysPath = Path.Combine(virtDirPhysPath, versionKey.DisplayText);
        var appPath = $"/{appName}/{versionKey.DisplayText}";
        var iisApp = site.Applications.FirstOrDefault(a => a.Path.Equals(appPath, StringComparison.OrdinalIgnoreCase));
        if (iisApp == null)
        {
            Console.WriteLine($"Adding application '{appPath}'");
            iisApp = site.Applications.Add(appPath, appPhysPath);
            iisApp.ApplicationPoolName = appPoolName;
        }
        server.CommitChanges();
        if (!Directory.Exists(appPhysPath))
        {
            Directory.CreateDirectory(appPhysPath);
        }
        await writeAppOffline(appKey, versionKey);
        await Task.Delay(10000);
    }

    private async Task<CredentialValue> retrieveCredentials(string credentialKey)
    {
        var credentialsFactory = scopes.GetRequiredService<ISecretCredentialsFactory>();
        var credentials = await credentialsFactory.Create(credentialKey).Value();
        return credentials;
    }

    private async Task writeAppOffline(AppKey appKey, AppVersionKey versionKey)
    {
        var offlinePath = getAppOfflinePath(appKey, versionKey);
        using (var writer = new StreamWriter(offlinePath, false))
        {
            await writer.WriteAsync
            (
@"
<html>
    <head>
        <title>App Offline</title>
    </head>
    <body>
        <h1>Web App is Temporarily Offline</h1>
    </body>
</html>
"
            );
        }
    }

    private string getAppOfflinePath(AppKey appKey, AppVersionKey installVersionKey)
    {
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
        var installDir = xtiFolder.InstallPath(appKey, installVersionKey);
        var appOfflinePath = Path.Combine(installDir, appOfflineFileName);
        return appOfflinePath;
    }

}
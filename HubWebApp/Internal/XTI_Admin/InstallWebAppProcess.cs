using Microsoft.Web.Administration;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Credentials;
using XTI_Secrets;
using XTI_WebAppInstallation;

namespace XTI_Admin;

internal sealed class InstallWebAppProcess : InstallAppProcess
{
    private readonly Scopes scopes;

    public InstallWebAppProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run(string publishedAppDir, AdminInstallOptions adminInstOptions, AppVersionKey installVersionKey)
    {
        Console.WriteLine($"Installing {adminInstOptions.AppKey.Name.DisplayText} {adminInstOptions.VersionKey.DisplayText} to website {adminInstOptions.Options.SiteName}");
        var appOfflineFile = new AppOfflineFile(scopes.GetRequiredService<XtiFolder>(), adminInstOptions.AppKey, installVersionKey);
        await prepareIis(adminInstOptions.AppKey, installVersionKey, adminInstOptions.Options.SiteName, appOfflineFile);
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
        File.Delete(appOfflineFile.FilePath);
    }

    private void deleteExistingWebFiles(AppKey appKey, AppVersionKey installVersionKey)
    {
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
        var installDir = xtiFolder.InstallPath(appKey, installVersionKey);
        Console.WriteLine($"Deleting files in '{installDir}'");
        var files = Directory.GetFiles(installDir)
            .Where(f => !Path.GetFileName(f).Equals(AppOfflineFile.FileName, StringComparison.OrdinalIgnoreCase));
        foreach (var file in files)
        {
            File.Delete(file);
        }
        foreach (var directory in Directory.GetDirectories(installDir))
        {
            Directory.Delete(directory, true);
        }
    }

    private async Task prepareIis(AppKey appKey, AppVersionKey versionKey, string siteName, AppOfflineFile appOfflineFile)
    {
        var secretCredentialsValue = await retrieveCredentials("WebApp");
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var iisWebSite = new IisWebSite(xtiFolder, xtiEnv, appKey, versionKey, siteName);
        await iisWebSite.CreateOrUpdate(secretCredentialsValue.UserName, secretCredentialsValue.Password);
    }

    private async Task<CredentialValue> retrieveCredentials(string credentialKey)
    {
        var credentialsFactory = scopes.GetRequiredService<ISecretCredentialsFactory>();
        var credentials = await credentialsFactory.Create(credentialKey).Value();
        return credentials;
    }
}
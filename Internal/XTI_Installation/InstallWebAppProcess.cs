using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub.Abstractions;
using XTI_Secrets;

namespace XTI_Installation;

public sealed class InstallWebAppProcess : InstallAppProcess
{
    private readonly XtiFolder xtiFolder;
    private readonly XtiEnvironment xtiEnv;
    private readonly ISecretCredentialsFactory credentialsFactory;

    public InstallWebAppProcess(XtiFolder xtiFolder, XtiEnvironment xtiEnv, ISecretCredentialsFactory credentialsFactory)
    {
        this.xtiFolder = xtiFolder;
        this.xtiEnv = xtiEnv;
        this.credentialsFactory = credentialsFactory;
    }

    public async Task Run(string publishedAppDir, InstallConfigurationModel installConfig, AppVersionKey installVersionKey, CancellationToken ct)
    {
        Console.WriteLine($"Installing {installConfig.AppKey.Name.DisplayText} {installVersionKey.DisplayText} to website {installConfig.Template.SiteName}");
        var appOfflineFile = new AppOfflineFile(xtiFolder, installConfig.AppKey, installVersionKey);
        await PrepareIis(installConfig.AppKey, installVersionKey, installConfig.Template.SiteName);
        try
        {
            DeleteExistingWebFiles(installConfig.AppKey, installVersionKey);
        }
        catch
        {
            await Task.Delay(TimeSpan.FromSeconds(15), ct);
            await RetryDelete(installConfig, installVersionKey, ct);
        }
        await new CopyToInstallDirProcess(xtiFolder).Run(publishedAppDir, installConfig.AppKey, installVersionKey, false);
        appOfflineFile.Delete();
    }

    private async Task RetryDelete(InstallConfigurationModel adminInstOptions, AppVersionKey installVersionKey, CancellationToken ct)
    {
        try
        {
            DeleteExistingWebFiles(adminInstOptions.AppKey, installVersionKey);
        }
        catch
        {
            await Task.Delay(TimeSpan.FromSeconds(15), ct);
            DeleteExistingWebFiles(adminInstOptions.AppKey, installVersionKey);
        }
    }

    private void DeleteExistingWebFiles(AppKey appKey, AppVersionKey installVersionKey)
    {
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

    private async Task PrepareIis(AppKey appKey, AppVersionKey versionKey, string siteName)
    {
        var secretCredentialsValue = await retrieveCredentials("WebApp");
        var iisWebSite = new IisWebSite(xtiFolder, xtiEnv, appKey, versionKey, siteName);
        await iisWebSite.CreateOrUpdate(secretCredentialsValue.UserName, secretCredentialsValue.Password);
    }

    private Task<CredentialValue> retrieveCredentials(string credentialKey) =>
        credentialsFactory.Create(credentialKey).Value();
}
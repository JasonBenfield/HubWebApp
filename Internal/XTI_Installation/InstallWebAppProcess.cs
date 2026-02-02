using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_Secrets;

namespace XTI_Installation;

public sealed class InstallWebAppProcess : InstallAppProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly ISecretCredentialsFactory credentialsFactory;

    internal InstallWebAppProcess(XtiFolder xtiFolder, IHubAdministration hubAdministration, XtiEnvironment xtiEnv, ISecretCredentialsFactory credentialsFactory)
        : base(xtiFolder, hubAdministration)
    {
        this.xtiEnv = xtiEnv;
        this.credentialsFactory = credentialsFactory;
    }

    protected override async Task _Run(string publishedAppDir, AppVersionInstallationModel versionInstallation, CancellationToken ct)
    {
        var versionKey = versionInstallation.GetVersionKey();
        var appOfflineFile = new AppOfflineFile(xtiFolder, versionInstallation.App.AppKey, versionKey);
        await PrepareIis(versionInstallation.App.AppKey, versionKey, versionInstallation.Installation.SiteName);
        try
        {
            DeleteExistingWebFiles(versionInstallation.App.AppKey, versionKey);
        }
        catch
        {
            await Task.Delay(TimeSpan.FromSeconds(15), ct);
            await RetryDelete(versionInstallation.App.AppKey, versionKey, ct);
        }
        await new CopyToInstallDirProcess(xtiFolder).Run(publishedAppDir, versionInstallation.App.AppKey, versionKey, false);
        appOfflineFile.Delete();
    }

    private async Task RetryDelete(AppKey appKey, AppVersionKey installVersionKey, CancellationToken ct)
    {
        try
        {
            DeleteExistingWebFiles(appKey, installVersionKey);
        }
        catch
        {
            await Task.Delay(TimeSpan.FromSeconds(15), ct);
            DeleteExistingWebFiles(appKey, installVersionKey);
        }
    }

    private void DeleteExistingWebFiles(AppKey appKey, AppVersionKey installVersionKey)
    {
        var installDir = xtiFolder.InstallPath(appKey, installVersionKey);
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
        var credentials = await credentialsFactory.Create("WebApp").Value();
        var iisWebSite = new IisWebSite(xtiFolder, xtiEnv, appKey, versionKey, siteName);
        await iisWebSite.CreateOrUpdate(credentials.UserName, credentials.Password);
    }

}
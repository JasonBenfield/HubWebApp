using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Secrets;

namespace XTI_Installation;

public sealed class InstallWebAppProcess : InstallAppProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiFolder xtiFolder;
    private readonly ISecretCredentialsFactory credentialsFactory;

    public InstallWebAppProcess(XtiEnvironment xtiEnv, XtiFolder xtiFolder, ISecretCredentialsFactory credentialsFactory)
    {
        this.xtiEnv = xtiEnv;
        this.xtiFolder = xtiFolder;
        this.credentialsFactory = credentialsFactory;
    }

    public async Task Run(string publishedAppDir, RequestedInstallation requestedInstallation, CancellationToken ct)
    {
        var appOfflineFile = new AppOfflineFile(xtiFolder, requestedInstallation.AppKey, requestedInstallation.VersionKey);
        await requestedInstallation.RunStep
        (
            "Prepare IIS",
            () => PrepareIis
            (
                requestedInstallation.AppKey,
                requestedInstallation.VersionKey,
                requestedInstallation.SiteName,
                ct
            ),
            ct
        );
        await requestedInstallation.RunStep
        (
            "Delete Existing Files",
            async () =>
            {
                try
                {
                    DeleteExistingWebFiles(requestedInstallation.AppKey, requestedInstallation.VersionKey);
                }
                catch
                {
                    await Task.Delay(TimeSpan.FromSeconds(15), ct);
                    await RetryDelete(requestedInstallation.AppKey, requestedInstallation.VersionKey, ct);
                }
            },
            ct
        );
        var installDir = xtiFolder.InstallPath(requestedInstallation.AppKey, requestedInstallation.VersionKey);
        await requestedInstallation.RunStep
        (
            $"Copy '{publishedAppDir}' to '{installDir}'",
            () => new CopyToInstallDirProcess(xtiFolder).Run
            (
                publishedAppDir,
                requestedInstallation.AppKey,
                requestedInstallation.VersionKey,
                false
            ),
            ct
        );
        await requestedInstallation.RunStep
        (
            "Delete Existing Files",
            () =>
            {
                appOfflineFile.Delete();
                return Task.CompletedTask;
            },
            ct
        );
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
            .Where(f => !Path.GetFileName(f).Equals(AppOfflineFile.FileName, StringComparison.OrdinalIgnoreCase))
            .ToArray();
        foreach (var file in files)
        {
            File.Delete(file);
        }
        foreach (var directory in Directory.GetDirectories(installDir))
        {
            Directory.Delete(directory, true);
        }
    }

    private async Task PrepareIis(AppKey appKey, AppVersionKey versionKey, string siteName, CancellationToken ct)
    {
        var credentials = await credentialsFactory.Create("WebApp").Value();
        var iisWebSite = new IisWebSite(xtiFolder, xtiEnv, appKey, versionKey, siteName);
        await iisWebSite.CreateOrUpdate(credentials.UserName, credentials.Password, ct);
    }

}
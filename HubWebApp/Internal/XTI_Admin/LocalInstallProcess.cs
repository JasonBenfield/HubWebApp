using System.IO.Compression;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Credentials;
using XTI_GitHub;
using XTI_Processes;

namespace XTI_Admin;

internal sealed class LocalInstallProcess
{
    private readonly Scopes scopes;
    private readonly AppKey appKey;

    public LocalInstallProcess(Scopes scopes, AppKey appKey)
    {
        this.scopes = scopes;
        this.appKey = appKey;
    }

    public async Task Run()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        Console.WriteLine($"Starting install {appKey.Name.DisplayText} {appKey.Type.DisplayText} {options.VersionKey} {options.Release}");
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var tempDir = Path.Combine
        (
            Path.GetTempPath(),
            $"xti_{appKey.Name.Value}_{appKey.Type.DisplayText.Replace(" ", "")}"
        );
        try
        {
            await storeInstallationUserCredentials();
            var versionKey = AppVersionKey.Current;
            if (xtiEnv.IsProduction() && !string.IsNullOrWhiteSpace(options.VersionKey))
            {
                versionKey = AppVersionKey.Parse(options.VersionKey);
            }
            var installationSource = options.InstallationSource;
            if (installationSource == InstallationSources.Default)
            {
                if (xtiEnv.IsDevelopmentOrTest())
                {
                    installationSource = InstallationSources.Folder;
                }
                else
                {
                    installationSource = InstallationSources.GitHub;
                }
            }
            if (installationSource == InstallationSources.Folder)
            {
                await copyPublishedDirToTempDir(tempDir, appKey, versionKey);
            }
            else
            {
                Console.WriteLine("Downloading Assets");
                await downloadAssets(tempDir);
            }
            await runSetup(options, xtiEnv, tempDir);
            if (appKey.Type.Equals(AppType.Values.WebApp))
            {
                if (xtiEnv.IsProduction())
                {
                    await new InstallWebAppProcess(scopes).Run(tempDir, appKey, versionKey, versionKey);
                }
                await new InstallWebAppProcess(scopes).Run(tempDir, appKey, versionKey, AppVersionKey.Current);
            }
            else if (appKey.Type.Equals(AppType.Values.ServiceApp))
            {
                if (xtiEnv.IsProduction())
                {
                    await new InstallServiceProcess(scopes).Run(tempDir, appKey, versionKey, versionKey);
                }
                await new InstallServiceProcess(scopes).Run(tempDir, appKey, versionKey, AppVersionKey.Current);
            }
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
        Console.WriteLine("Installation Complete");
    }

    private async Task runSetup(AdminOptions options, XtiEnvironment xtiEnv, string tempDir)
    {
        var setupAppDir = Path.Combine(tempDir, "Setup");
        var setupResult = await new XtiProcess(Path.Combine(setupAppDir, $"{appKey.Name.DisplayText}SetupApp.exe"))
            .UseEnvironment(xtiEnv.EnvironmentName)
            .WriteOutputToConsole()
            .AddConfigOptions
            (
                new
                {
                    VersionKey = options.VersionKey,
                    VersionsPath = Path.Combine(tempDir, "versions.json"),
                    Domain = options.Domain
                },
                "Setup"
            )
            .Run();
        setupResult.EnsureExitCodeIsZero();
    }

    private async Task copyPublishedDirToTempDir(string tempDir, AppKey appKey, AppVersionKey versionKey)
    {
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
        var sourceDir = xtiFolder.PublishPath(appKey, versionKey);
        var copyProcess = new RobocopyProcess(sourceDir, tempDir)
            .CopySubdirectoriesIncludingEmpty()
            .NoDirectoryLogging()
            .NoFileClassLogging()
            .NoFileLogging()
            .NoFileSizeLogging()
            .NoJobHeader()
            .NoJobSummary()
            .NoProgressDisplayed()
            .Purge();
        await copyProcess.Run();
    }

    private async Task downloadAssets(string tempDir)
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
        if (Directory.Exists(tempDir))
        {
            Directory.Delete(tempDir, true);
        }
        Directory.CreateDirectory(tempDir);
        var appKeyText =  $"{appKey.Name.DisplayText}{appKey.Type.DisplayText}".Replace(" ", "");
        var release = await gitHubRepo.Release(options.Release);
        var setupAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}Setup.zip", StringComparison.OrdinalIgnoreCase));
        if (setupAsset != null)
        {
            var versionsAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}Versions.json", StringComparison.OrdinalIgnoreCase));
            if (versionsAsset != null)
            {
                Console.WriteLine($"Downloading versions.json {release.TagName} {setupAsset.Name}");
                var versionsContent = await gitHubRepo.DownloadReleaseAsset(versionsAsset);
                var versionsPath = Path.Combine(tempDir, "versions.json");
                await File.WriteAllBytesAsync(versionsPath, versionsContent);
            }
            Console.WriteLine($"Downloading Setup {release.TagName} {setupAsset.Name}");
            var setupContent = await gitHubRepo.DownloadReleaseAsset(setupAsset);
            var setupZipPath = Path.Combine(tempDir, "setup.zip");
            await File.WriteAllBytesAsync(setupZipPath, setupContent);
            var setupAppDir = Path.Combine(tempDir, "Setup");
            ZipFile.ExtractToDirectory(setupZipPath, setupAppDir);
        }
        var appAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}.zip", StringComparison.OrdinalIgnoreCase));
        if (appAsset != null)
        {
            Console.WriteLine($"Downloading App {release.TagName} {appAsset.Name}");
            var appContent = await gitHubRepo.DownloadReleaseAsset(appAsset);
            var appZipPath = Path.Combine(tempDir, "setup.zip");
            await File.WriteAllBytesAsync(appZipPath, appContent);
            var appDir = Path.Combine(tempDir, "App");
            ZipFile.ExtractToDirectory(appZipPath, appDir);
        }
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
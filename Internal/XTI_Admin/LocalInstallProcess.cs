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

    public LocalInstallProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        var appKey = options.AppKey();
        Console.WriteLine($"Starting install {options.AppName} {options.AppType} {options.VersionKey} {options.Release}");
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
            if (xtiEnv.IsDevelopmentOrTest())
            {
                await runSetup(appKey, versionKey);
            }
            else
            {
                Console.WriteLine("Downloading Assets");
                await downloadAssets(tempDir);
            }
            if (appKey.Type.Equals(AppType.Values.WebApp))
            {
                if (xtiEnv.IsProduction())
                {
                    await new InstallWebAppProcess(scopes).Run(tempDir, versionKey, versionKey);
                }
                await new InstallWebAppProcess(scopes).Run(tempDir, versionKey, AppVersionKey.Current);
            }
            else if (appKey.Type.Equals(AppType.Values.ServiceApp))
            {
                if (xtiEnv.IsProduction())
                {
                    await new InstallServiceProcess(scopes).Run(tempDir, versionKey, versionKey);
                }
                await new InstallServiceProcess(scopes).Run(tempDir, versionKey, AppVersionKey.Current);
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

    private async Task runSetup(AppKey appKey, AppVersionKey versionKey)
    {
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
        var sourceDir = xtiFolder.PublishPath(appKey, versionKey);
        var setupAppDir = Path.Combine(sourceDir, "Setup");
        Console.WriteLine($"Running Setup '{setupAppDir}'");
        if (Directory.Exists(setupAppDir))
        {
            var appName = appKey.Name.DisplayText.Replace(" ", "");
            var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
            var result = await new XtiProcess(Path.Combine(setupAppDir, $"{appName}SetupApp.exe"))
                .SetWorkingDirectory(setupAppDir)
                .UseEnvironment(xtiEnv.EnvironmentName)
                .AddConfigOptions
                (
                    new
                    {
                        VersionKey = versionKey.DisplayText,
                        VersionsPath = Path.Combine(sourceDir, "versions.json"),
                        Domain = scopes.GetRequiredService<AdminOptions>().Domain
                    },
                    "Setup"
                )
                .WriteOutputToConsole()
                .Run();
            result.EnsureExitCodeIsZero();
        }
        else
        {
            Console.WriteLine($"Setup App not Found '{setupAppDir}'");
        }
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
        var release = await gitHubRepo.Release(options.Release);
        var setupAsset = release.Assets.FirstOrDefault(a => a.Name.Equals("setup.zip", StringComparison.OrdinalIgnoreCase));
        if (setupAsset != null)
        {
            var versionsAsset = release.Assets.FirstOrDefault(a => a.Name.Equals("versions.json", StringComparison.OrdinalIgnoreCase));
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
            var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
            Console.WriteLine($"Running Setup '{setupAppDir}'");
            var setupResult = await new XtiProcess(Path.Combine(setupAppDir, $"{options.AppName}SetupApp.exe"))
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
        var appAsset = release.Assets.FirstOrDefault(a => a.Name.Equals("app.zip", StringComparison.OrdinalIgnoreCase));
        if (appAsset != null)
        {
            Console.WriteLine($"Downloading App {release.TagName} {appAsset.Name}");
            var appContent = await gitHubRepo.DownloadReleaseAsset(appAsset);
            var appZipPath = Path.Combine(tempDir, "setup.zip");
            await File.WriteAllBytesAsync(appZipPath, appContent);
            var setupAppDir = Path.Combine(tempDir, "App");
            ZipFile.ExtractToDirectory(appZipPath, setupAppDir);
        }
    }

    private Task storeInstallationUserCredentials()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        var credentials = scopes.GetRequiredService<InstallationUserCredentials>();
        return credentials.Update
        (
            new CredentialValue
            (
                options.InstallationUserName,
                options.InstallationPassword
            )
        );
    }

}
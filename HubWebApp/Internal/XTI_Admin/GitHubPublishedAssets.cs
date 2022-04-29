using System.IO.Compression;
using XTI_App.Abstractions;
using XTI_GitHub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class GitHubPublishedAssets : IPublishedAssets
{
    private readonly AdminOptions options;
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly AppVersionName versionName;
    private string tempDir = "";

    public GitHubPublishedAssets(AdminOptions options, XtiGitHubRepository gitHubRepo, AppVersionNameAccessor versionNameAccessor)
    {
        this.options = options;
        this.gitHubRepo = gitHubRepo;
        versionName = versionNameAccessor.Value;
        tempDir = Path.Combine
        (
            Path.GetTempPath(),
            $"xti_{versionName.Value}"
        );
    }

    public string VersionsPath { get; private set; } = "";

    public string SetupAppPath { get; private set; } = "";

    public string AppPath { get; private set; } = "";

    public async Task LoadSetup(AppKey appKey, AppVersionKey versionKey)
    {
        SetupAppPath = "";
        AppPath = "";
        var appTempDir = prepareTempDir(appKey);
        var appKeyText = getAppKeyText(appKey);
        var release = await getRelease();
        await downloadSetup(appTempDir, appKeyText, release);
    }

    public async Task LoadApps(AppKey appKey, AppVersionKey versionKey)
    {
        SetupAppPath = "";
        AppPath = "";
        var appTempDir = prepareTempDir(appKey);
        var appKeyText = getAppKeyText(appKey);
        var release = await getRelease();
        await downloadSetup(appTempDir, appKeyText, release);
        await downloadApp(appTempDir, appKeyText, release);
    }

    private string prepareTempDir(AppKey appKey)
    {
        var appTempDir = Path.Combine
        (
            tempDir,
            $"xti_{appKey.Name.Value}_{appKey.Type.DisplayText.Replace(" ", "")}"
        );
        if (Directory.Exists(appTempDir))
        {
            Directory.Delete(appTempDir, true);
        }
        Directory.CreateDirectory(appTempDir);
        return appTempDir;
    }

    private static string getAppKeyText(AppKey appKey) => $"{appKey.Name.DisplayText}{appKey.Type.DisplayText}".Replace(" ", "");

    private async Task downloadSetup(string appTempDir, string appKeyText, GitHubRelease release)
    {
        var setupAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}Setup.zip", StringComparison.OrdinalIgnoreCase));
        if (setupAsset != null)
        {
            Console.WriteLine($"Downloading Setup {release.TagName} {setupAsset.Name}");
            var setupContent = await gitHubRepo.DownloadReleaseAsset(setupAsset);
            var setupZipPath = Path.Combine(appTempDir, "setup.zip");
            await File.WriteAllBytesAsync(setupZipPath, setupContent);
            SetupAppPath = Path.Combine(appTempDir, "Setup");
            ZipFile.ExtractToDirectory(setupZipPath, SetupAppPath);
        }
    }

    private async Task downloadApp(string appTempDir, string appKeyText, GitHubRelease release)
    {
        var appAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}.zip", StringComparison.OrdinalIgnoreCase));
        if (appAsset != null)
        {
            Console.WriteLine($"Downloading App {release.TagName} {appAsset.Name}");
            var appContent = await gitHubRepo.DownloadReleaseAsset(appAsset);
            var appZipPath = Path.Combine(appTempDir, "setup.zip");
            await File.WriteAllBytesAsync(appZipPath, appContent);
            AppPath = Path.Combine(appTempDir, "App");
            ZipFile.ExtractToDirectory(appZipPath, AppPath);
        }
    }

    private Task<GitHubRelease> getRelease() => gitHubRepo.Release(options.Release);

    public async Task LoadVersions()
    {
        VersionsPath = "";
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }
        var versionsPath = Path.Combine(tempDir, "versions.json");
        if (File.Exists(versionsPath))
        {
            File.Delete(versionsPath);
        }
        var release = await gitHubRepo.Release(options.Release);
        var versionsAsset = release.Assets.FirstOrDefault(a => a.Name.Equals("versions.json", StringComparison.OrdinalIgnoreCase));
        if (versionsAsset != null)
        {
            Console.WriteLine($"Downloading versions.json {release.TagName} {versionsAsset.Name}");
            var versionsContent = await gitHubRepo.DownloadReleaseAsset(versionsAsset);
            VersionsPath = versionsPath;
            await File.WriteAllBytesAsync(VersionsPath, versionsContent);
        }
    }

    public void Dispose()
    {
        if (!string.IsNullOrWhiteSpace(tempDir) && Directory.Exists(tempDir))
        {
            Directory.Delete(tempDir, true);
        }
    }

}

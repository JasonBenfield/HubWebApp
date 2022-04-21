using System.IO.Compression;
using XTI_App.Abstractions;
using XTI_GitHub;

namespace XTI_Admin;

public sealed class GitHubPublishedAssets : IPublishedAssets
{
    private readonly AdminOptions options;
    private readonly XtiGitHubRepository gitHubRepo;
    private string tempDir = "";

    public GitHubPublishedAssets(AdminOptions options, XtiGitHubRepository gitHubRepo)
    {
        this.options = options;
        this.gitHubRepo = gitHubRepo;
    }

    public string VersionsPath { get; private set; } = "";

    public string SetupAppPath { get; private set; } = "";

    public string AppPath { get; private set; } = "";

    public async Task Load(AppKey appKey, AppVersionKey versionKey)
    {
        tempDir = Path.Combine
        (
            Path.GetTempPath(),
            $"xti_{appKey.Name.Value}_{appKey.Type.DisplayText.Replace(" ", "")}"
        );
        if (Directory.Exists(tempDir))
        {
            Directory.Delete(tempDir, true);
        }
        Directory.CreateDirectory(tempDir);
        var appKeyText = $"{appKey.Name.DisplayText}{appKey.Type.DisplayText}".Replace(" ", "");
        var release = await gitHubRepo.Release(options.Release);
        var versionsAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}Versions.json", StringComparison.OrdinalIgnoreCase));
        if (versionsAsset != null)
        {
            Console.WriteLine($"Downloading versions.json {release.TagName} {versionsAsset.Name}");
            var versionsContent = await gitHubRepo.DownloadReleaseAsset(versionsAsset);
            VersionsPath = Path.Combine(tempDir, "versions.json");
            await File.WriteAllBytesAsync(VersionsPath, versionsContent);
        }
        var setupAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}Setup.zip", StringComparison.OrdinalIgnoreCase));
        if (setupAsset != null)
        {
            Console.WriteLine($"Downloading Setup {release.TagName} {setupAsset.Name}");
            var setupContent = await gitHubRepo.DownloadReleaseAsset(setupAsset);
            var setupZipPath = Path.Combine(tempDir, "setup.zip");
            await File.WriteAllBytesAsync(setupZipPath, setupContent);
            SetupAppPath = Path.Combine(tempDir, "Setup");
            ZipFile.ExtractToDirectory(setupZipPath, SetupAppPath);
        }
        var appAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}.zip", StringComparison.OrdinalIgnoreCase));
        if (appAsset != null)
        {
            Console.WriteLine($"Downloading App {release.TagName} {appAsset.Name}");
            var appContent = await gitHubRepo.DownloadReleaseAsset(appAsset);
            var appZipPath = Path.Combine(tempDir, "setup.zip");
            await File.WriteAllBytesAsync(appZipPath, appContent);
            AppPath = Path.Combine(tempDir, "App");
            ZipFile.ExtractToDirectory(appZipPath, AppPath);
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

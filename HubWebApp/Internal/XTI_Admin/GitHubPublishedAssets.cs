﻿using System.IO.Compression;
using XTI_App.Abstractions;
using XTI_GitHub;

namespace XTI_Admin;

public sealed class GitHubPublishedAssets : IPublishedAssets
{
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly AppVersionName versionName;
    private readonly CurrentVersion currentVersionAccessor;
    private string tempDir = "";
    private GitHubRelease? release;

    public GitHubPublishedAssets(XtiGitHubRepository gitHubRepo, AppVersionNameAccessor versionNameAccessor, CurrentVersion currentVersionAccessor)
    {
        this.gitHubRepo = gitHubRepo;
        versionName = versionNameAccessor.Value;
        this.currentVersionAccessor = currentVersionAccessor;
        tempDir = Path.Combine
        (
            Path.GetTempPath(),
            $"xti_{versionName.Value}"
        );
    }

    public async Task<string> LoadSetup(string releaseTag, AppKey appKey, AppVersionKey versionKey, CancellationToken ct)
    {
        var appTempDir = PrepareTempDir(appKey);
        var appKeyText = GetAppKeyText(appKey);
        var release = await GetRelease(releaseTag, ct);
        var setupPath = await DownloadSetup(appTempDir, appKeyText, release);
        return setupPath;
    }

    public async Task<string> LoadApps(string releaseTag, AppKey appKey, AppVersionKey versionKey, CancellationToken ct)
    {
        var appTempDir = PrepareTempDir(appKey);
        var appKeyText = GetAppKeyText(appKey);
        var release = await GetRelease(releaseTag, ct);
        var appPath = await DownloadApp(appTempDir, appKeyText, release);
        return appPath;
    }

    private async Task<string> DownloadSetup(string appTempDir, string appKeyText, GitHubRelease release)
    {
        var setupAppPath = "";
        var setupAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}Setup.zip", StringComparison.OrdinalIgnoreCase));
        if (setupAsset != null)
        {
            Console.WriteLine($"Downloading Setup {release.TagName} {setupAsset.Name}");
            var setupContent = await gitHubRepo.DownloadReleaseAsset(setupAsset);
            var setupZipPath = Path.Combine(appTempDir, "setup.zip");
            if (File.Exists(setupZipPath))
            {
                File.Delete(setupZipPath);
            }
            await File.WriteAllBytesAsync(setupZipPath, setupContent);
            setupAppPath = Path.Combine(appTempDir, "Setup");
            if (Directory.Exists(setupAppPath))
            {
                Directory.Delete(setupAppPath, true);
            }
            Directory.CreateDirectory(setupAppPath);
            ZipFile.ExtractToDirectory(setupZipPath, setupAppPath);
        }
        return setupAppPath;
    }

    private async Task<string> DownloadApp(string appTempDir, string appKeyText, GitHubRelease release)
    {
        var appPath = "";
        var appAsset = release.Assets.FirstOrDefault(a => a.Name.Equals($"{appKeyText}.zip", StringComparison.OrdinalIgnoreCase));
        if (appAsset != null)
        {
            Console.WriteLine($"Downloading App {release.TagName} {appAsset.Name}");
            var appContent = await gitHubRepo.DownloadReleaseAsset(appAsset);
            var appZipPath = Path.Combine(appTempDir, "setup.zip");
            if (File.Exists(appZipPath))
            {
                File.Delete(appZipPath);
            }
            await File.WriteAllBytesAsync(appZipPath, appContent);
            appPath = Path.Combine(appTempDir, "App");
            if (Directory.Exists(appPath))
            {
                Directory.Delete(appPath, true);
            }
            Directory.CreateDirectory(appPath);
            ZipFile.ExtractToDirectory(appZipPath, appPath);
        }
        return appPath;
    }

    public async Task<string> LoadVersions(string releaseTag)
    {
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }
        var versionsPath = Path.Combine(tempDir, "versions.json");
        if (File.Exists(versionsPath))
        {
            File.Delete(versionsPath);
        }
        var release = await gitHubRepo.Release(releaseTag);
        var versionsAsset = release.Assets.FirstOrDefault(a => a.Name.Equals("versions.json", StringComparison.OrdinalIgnoreCase));
        if (versionsAsset != null)
        {
            Console.WriteLine($"Downloading versions.json {release.TagName} {versionsAsset.Name}");
            var versionsContent = await gitHubRepo.DownloadReleaseAsset(versionsAsset);
            await File.WriteAllBytesAsync(versionsPath, versionsContent);
        }
        return versionsPath;
    }

    private static string GetAppKeyText(AppKey appKey) => $"{appKey.Name.DisplayText}{appKey.Type.DisplayText}".Replace(" ", "");

    private string PrepareTempDir(AppKey appKey)
    {
        var appTempDir = Path.Combine
        (
            tempDir,
            $"xti_{appKey.Name.Value}_{appKey.Type.DisplayText.Replace(" ", "")}"
        );
        if (!Directory.Exists(appTempDir))
        {
            Directory.CreateDirectory(appTempDir);
        }
        return appTempDir;
    }

    private async Task<GitHubRelease> GetRelease(string releaseTag, CancellationToken ct)
    {
        if (release == null || release.TagName != releaseTag)
        {
            if (string.IsNullOrWhiteSpace(releaseTag))
            {
                var currentVersion = await currentVersionAccessor.Value(ct);
                releaseTag = $"v{currentVersion.VersionNumber.Format()}";
            }
            release = await gitHubRepo.Release(releaseTag);
        }
        return release;
    }

    public void Dispose()
    {
        if (!string.IsNullOrWhiteSpace(tempDir) && Directory.Exists(tempDir))
        {
            Directory.Delete(tempDir, true);
        }
    }

}

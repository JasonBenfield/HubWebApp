using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed class FolderPublishedAssets : IPublishedAssets
{
    private readonly PublishedFolder publishFolder;

    public FolderPublishedAssets(PublishedFolder publishFolder)
    {
        this.publishFolder = publishFolder;
    }

    public Task<string> LoadVersions(CancellationToken ct)
    {
        var versionsPath = publishFolder.VersionsPath();
        return Task.FromResult(versionsPath);
    }

    public Task<string> LoadSetup(string releaseTag, AppKey appKey, AppVersionKey versionKey, CancellationToken ct)
    {
        var sourceDir = publishFolder.AppDir(appKey, versionKey);
        var setupAppPath = Path.Combine(sourceDir, "Setup");
        return Task.FromResult(setupAppPath);
    }

    public Task<string> LoadApps(string releaseTag, AppKey appKey, AppVersionKey versionKey, CancellationToken ct)
    {
        var sourceDir = publishFolder.AppDir(appKey, versionKey);
        var appPath = Path.Combine(sourceDir, "App");
        return Task.FromResult(appPath);
    }

    public void Dispose() { }
}

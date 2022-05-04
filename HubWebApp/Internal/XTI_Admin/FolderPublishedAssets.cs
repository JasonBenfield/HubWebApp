using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed class FolderPublishedAssets : IPublishedAssets
{
    private readonly PublishedFolder publishFolder;

    public FolderPublishedAssets(PublishedFolder publishFolder)
    {
        this.publishFolder = publishFolder;
    }

    public Task<string> LoadVersions(string releaseTag)
    {
        var versionsPath = publishFolder.VersionsPath();
        return Task.FromResult(versionsPath);
    }

    public Task<string> LoadSetup(AppKey appKey, AppVersionKey versionKey)
    {
        var sourceDir = publishFolder.AppDir(appKey, versionKey);
        var setupAppPath = Path.Combine(sourceDir, "Setup");
        return Task.FromResult(setupAppPath);
    }

    public Task<string> LoadApps(AppKey appKey, AppVersionKey versionKey)
    {
        var sourceDir = publishFolder.AppDir(appKey, versionKey);
        var appPath = Path.Combine(sourceDir, "App");
        return Task.FromResult(appPath);
    }

    public Task<string> LoadTools(AppKey appKey, AppVersionKey versionKey)
    {
        var sourceDir = publishFolder.AppDir(appKey, versionKey);
        var toolsPath = Path.Combine(sourceDir, "Tools");
        return Task.FromResult(toolsPath);
    }

    public Task<string> LoadPowershell(AppKey appKey, AppVersionKey versionKey)
    {
        var sourceDir = publishFolder.AppDir(appKey, versionKey);
        var psPath = Path.Combine(sourceDir, "Powershell");
        return Task.FromResult(psPath);
    }

    public void Dispose() { }
}

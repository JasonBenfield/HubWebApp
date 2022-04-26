using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed class FolderPublishedAssets : IPublishedAssets
{
    private readonly PublishFolder publishFolder;

    public FolderPublishedAssets(PublishFolder publishFolder)
    {
        this.publishFolder = publishFolder;
    }

    public string VersionsPath { get; private set; } = "";

    public string SetupAppPath { get; private set; } = "";

    public string AppPath { get; private set; } = "";

    public Task LoadVersions()
    {
        VersionsPath = publishFolder.VersionsPath();
        return Task.CompletedTask;
    }

    public Task LoadApps(AppKey appKey, AppVersionKey versionKey)
    {
        var sourceDir = publishFolder.AppDir(appKey, versionKey);
        SetupAppPath = Path.Combine(sourceDir, "Setup");
        AppPath = Path.Combine(sourceDir, "App");
        return Task.CompletedTask;
    }

    public void Dispose() { }
}

using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;

namespace XTI_Admin;

public sealed class FolderPublishedAssets : IPublishedAssets
{
    private readonly XtiFolder xtiFolder;

    public FolderPublishedAssets(XtiFolder xtiFolder)
    {
        this.xtiFolder = xtiFolder;
    }

    public string VersionsPath { get; private set; } = "";

    public string SetupAppPath { get; private set; } = "";

    public string AppPath { get; private set; } = "";

    public Task Load(AppKey appKey, AppVersionKey versionKey)
    {
        var sourceDir = xtiFolder.PublishPath(appKey, versionKey);
        VersionsPath = Path.Combine(sourceDir, "versions.json");
        SetupAppPath = Path.Combine(sourceDir, "Setup");
        AppPath = Path.Combine(sourceDir, "App");
        return Task.CompletedTask;
    }

    public void Dispose() { }

}

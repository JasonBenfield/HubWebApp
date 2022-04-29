using XTI_App.Abstractions;

namespace XTI_Admin;

public interface IPublishedAssets : IDisposable
{
    string VersionsPath { get; }
    string SetupAppPath { get; }
    string AppPath { get; }

    Task LoadVersions();

    Task LoadSetup(AppKey appKey, AppVersionKey versionKey);

    Task LoadApps(AppKey appKey, AppVersionKey versionKey);
}

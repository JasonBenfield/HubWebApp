using XTI_App.Abstractions;

namespace XTI_Admin;

public interface IPublishedAssets : IDisposable
{
    string VersionsPath { get; }
    string SetupAppPath { get; }
    string AppPath { get; }

    Task Load(AppKey appKey, AppVersionKey versionKey);
}

using XTI_App.Abstractions;

namespace XTI_Admin;

public interface IPublishedAssets : IDisposable
{
    Task<string> LoadVersions(string releaseTag);

    Task<string> LoadSetup(AppKey appKey, AppVersionKey versionKey);

    Task<string> LoadApps(AppKey appKey, AppVersionKey versionKey);
}

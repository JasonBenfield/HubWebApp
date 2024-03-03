using XTI_App.Abstractions;

namespace XTI_Admin;

public interface IPublishedAssets : IDisposable
{
    Task<string> LoadVersions(string releaseTag);

    Task<string> LoadSetup(string releaseTag, AppKey appKey, AppVersionKey versionKey, CancellationToken ct);

    Task<string> LoadApps(string releaseTag, AppKey appKey, AppVersionKey versionKey, CancellationToken ct);
}

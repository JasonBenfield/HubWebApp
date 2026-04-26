using XTI_App.Abstractions;

namespace XTI_Internal.Abstractions;

public interface IPublishedAssets : IDisposable
{
    Task<string> LoadVersions(CancellationToken ct);

    Task<string> LoadSetup(string releaseTag, AppKey appKey, AppVersionKey versionKey, CancellationToken ct);

    Task<string> LoadApps(string releaseTag, AppKey appKey, AppVersionKey versionKey, CancellationToken ct);
}

using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class GetVersionRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;
}
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class GetVersionRequest
{
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;
}
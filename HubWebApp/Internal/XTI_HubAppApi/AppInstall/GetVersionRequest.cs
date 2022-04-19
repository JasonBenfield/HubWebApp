using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class GetVersionRequest
{
    public string GroupName { get; set; } = "";
    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;
}
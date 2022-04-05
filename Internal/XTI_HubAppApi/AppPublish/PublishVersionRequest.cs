using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppPublish;

public sealed class PublishVersionRequest
{
    public string GroupName { get; set; } = "";
    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;
}
namespace XTI_HubAppApi.AppPublish;

public sealed class PublishVersionRequest
{
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;
}
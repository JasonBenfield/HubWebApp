using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppPublish;

public sealed class NewVersionRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.NotSet;
}
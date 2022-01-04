using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppPublish;

public sealed class NewVersionRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public string Domain { get; set; } = "";
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.NotSet;
}
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppPublish;

public sealed class NewVersionRequest
{
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.NotSet;
    public AppKey[] AppKeys { get; set; } = new AppKey[0];
}
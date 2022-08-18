namespace XTI_HubWebAppApi.AppPublish;

public sealed class NewVersionRequest
{
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.NotSet;
}
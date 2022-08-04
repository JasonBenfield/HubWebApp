namespace XTI_HubWebAppApi.AppInstall;

public sealed class GetVersionRequest
{
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;
}